using UnityEngine;

public class UnitMain : MonoBehaviour
{
    [SerializeField] private UnitType unitType = UnitType.Hero;
    [SerializeField] private UnitMovement unitMovement;
    [SerializeField] private UnitCollisionDetect unitCollisionDetect;
    [SerializeField] private UnitStatus unitStatus;
    [SerializeField] private UnitLevelProgression unitLevelProgression;
    [SerializeField] private HpBar hpBar;

    public UnitMovement UnitMovement => unitMovement;
    public UnitCollisionDetect UnitCollisionDetect => unitCollisionDetect;
    public UnitStatus UnitStatus => unitStatus;
    public UnitLevelProgression UnitLevelProgression => unitLevelProgression;
    public HpBar HpBar => hpBar;
    public Box CurrentBox => unitMovement.CurrentBox;

    public void Init(InfoInitUnit infoInitUnit)
    {
        unitType = infoInitUnit.unitType;
        unitMovement.Init(infoInitUnit.unitType, infoInitUnit.direction, infoInitUnit.box);
        unitCollisionDetect.Init(this, infoInitUnit.unitType);
        unitLevelProgression.Init(infoInitUnit.unitType, infoInitUnit.level);
        unitStatus.Init(infoInitUnit.unitType, infoInitUnit.level);

        unitLevelProgression.AssignOnUnitLevelUp(OnUnitLevelUp);
    }


    public void OnUnitRecruited()
    {
        unitMovement.enabled = false;
        unitCollisionDetect.DisableCollisionDetect();
    }

    private void OnUnitLevelUp(int level)
    {
        unitStatus.OnUnitLevelUp(level);
    }

    public void OnUnitDamaged(InfoDamage infoDamage)
    {
        unitStatus.OnUnitDamaged(infoDamage);
    }

    public void OnUnitHealed(int heal)
    {
        unitStatus.OnUnitHealed(heal);
    }

    public void OnUnitDie()
    {
        CustomDebug.SetMessage($"{unitType} is dead");
        if (unitType == UnitType.Hero)
        {
            // remove & rearrange units
            HeroHeadGroup.instance.OnHeroDie(this);
        }
        else
        {
            // remove & give reward
            CurrentBox.BoxStatus = BoxStatus.Empty;
            var exp = unitLevelProgression.CalculateExp();
            HeroHeadGroup.instance.OnEnemyDie(exp);
            this.gameObject.SetActive(false);
        }
    }
}