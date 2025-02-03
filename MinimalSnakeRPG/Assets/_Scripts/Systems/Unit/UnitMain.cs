using UnityEngine;

public class UnitMain : MonoBehaviour
{
    [SerializeField] protected UnitType unitType = UnitType.Hero;
    [SerializeField] protected UnitMovement unitMovement;
    [SerializeField] protected UnitCollisionDetect unitCollisionDetect;
    [SerializeField] protected UnitStatus unitStatus;
    [SerializeField] protected UnitLevelProgression unitLevelProgression;
    [SerializeField] protected HpBar hpBar;

    public UnitMovement UnitMovement => unitMovement;
    public UnitCollisionDetect UnitCollisionDetect => unitCollisionDetect;
    public UnitStatus UnitStatus => unitStatus;
    public UnitLevelProgression UnitLevelProgression => unitLevelProgression;
    public HpBar HpBar => hpBar;
    public Box CurrentBox => unitMovement.CurrentBox;

    public void Init(InfoInitUnit infoInitUnit)
    {
        unitType = infoInitUnit.unitType;
        unitMovement.Init(infoInitUnit);
        unitCollisionDetect.Init(this, infoInitUnit.unitType);
        unitStatus.Init(this, infoInitUnit);
        unitLevelProgression.Init(infoInitUnit);
        unitLevelProgression.AssignOnUnitLevelUp(OnUnitLevelUp);
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
        Destroy(gameObject);
    }
}