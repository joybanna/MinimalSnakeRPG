using UnityEngine;

public class UnitMain : MonoBehaviour
{
    [SerializeField] protected UnitType unitType = UnitType.Hero;
    [SerializeField] protected UnitMovement unitMovement;
    [SerializeField] protected UnitCollisionDetect unitCollisionDetect;
    [SerializeField] protected UnitStatus unitStatus;
    [SerializeField] protected UnitLevelProgression unitLevelProgression;
    [SerializeField] protected HpBar hpBar;
    [SerializeField] protected AbilityBase abilityBase;
    public UnitMovement UnitMovement => unitMovement;
    public UnitCollisionDetect UnitCollisionDetect => unitCollisionDetect;
    public UnitStatus UnitStatus => unitStatus;
    public UnitLevelProgression UnitLevelProgression => unitLevelProgression;
    public HpBar HpBar => hpBar;
    public Box CurrentBox => unitMovement.CurrentBox;
    public UnitType UnitType => unitType;

    [SerializeField] protected HandleClickShowDetail handleClickShowDetail;
    [SerializeField] protected VisualDir visualDir;

    public AbilityBase AbilityBase => abilityBase;

    public void Init(InfoInitUnit infoInitUnit)
    {
        unitType = infoInitUnit.unitType;
        unitMovement.Init(infoInitUnit);
        unitCollisionDetect.Init(this, infoInitUnit.unitType);
        unitStatus.Init(this, infoInitUnit);
        unitLevelProgression.Init(infoInitUnit);
        unitLevelProgression.AssignOnUnitLevelUp(OnUnitLevelUp);
        abilityBase.InitAbility(this);
        handleClickShowDetail.Init(this);
        UnitsCollector.instance.OnUnitEntry(unitType, this);
    }


    public void OnUnitRecruited()
    {
        unitMovement.enabled = false;
        unitCollisionDetect.DisableCollisionDetect();
    }

    protected virtual void OnUnitLevelUp(int level)
    {
        unitStatus.OnUnitLevelUp(level);
        handleClickShowDetail.UpdateCard();
        SoundController.instance.PlaySFX(SoundSource.LevelUp);
    }

    public virtual void OnUnitDamaged(InfoDamage infoDamage)
    {
        unitStatus.OnUnitDamaged(infoDamage);
        handleClickShowDetail.UpdateCard();
    }

    public virtual void OnUnitHealed(int heal)
    {
        CustomDebug.SetMessage("Heal " + heal + " to " + unitType, Color.green);
        unitStatus.OnUnitHealed(heal);
        handleClickShowDetail.UpdateCard();
    }

    public virtual void OnUnitDie()
    {
        CustomDebug.SetMessage($"{unitType} is dead");
        UnitsCollector.instance.OnUnitExit(unitType, this);
        SoundController.instance.PlaySFX(SoundSource.Die);
        Destroy(gameObject);
    }

    public void OnTurnEnd()
    {
        abilityBase.OnTurnEnd();
    }

    public void OnAttack(UnitMain unit)
    {
        abilityBase.OnAttack(unit);
    }

    public virtual void ShowArrowDir(bool isShow)
    {
        visualDir.ShowArrow(isShow);
    }
}