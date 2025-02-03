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

    public void Init(InfoInitUnit infoInitUnit)
    {
        unitType = infoInitUnit.unitType;
        unitMovement.Init(infoInitUnit);
        unitCollisionDetect.Init(this, infoInitUnit.unitType);
        unitStatus.Init(this, infoInitUnit);
        unitLevelProgression.Init(infoInitUnit);
        unitLevelProgression.AssignOnUnitLevelUp(OnUnitLevelUp);
        abilityBase.InitAbility(this);
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
        SoundController.instance.PlaySFX(SoundSource.LevelUp);
    }

    public virtual void OnUnitDamaged(InfoDamage infoDamage)
    {
        unitStatus.OnUnitDamaged(infoDamage);
    }

    public virtual void OnUnitHealed(int heal)
    {
        unitStatus.OnUnitHealed(heal);
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

    public void OnAttack()
    {
        abilityBase.OnAttack();
    }
}