using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    private UnitType _unitType;
    [SerializeField] private UnitClass unitClass = UnitClass.Warrior;
    public UnitClass UnitClass => unitClass;
    [SerializeField] private UnitStat _unitStat;
    public UnitStat UnitStat => _unitStat;

    [SerializeField] private UnitStat _bonusStat;
    public UnitStat BonusStat => _bonusStat;


    private int _currentHp;
    public int CurrentHp => _currentHp;
    public int MaxHp => _unitStat.hp + _bonusStat.hp;
    private UnitMain _unitMain;

    public void Init(UnitMain unitMain, InfoInitUnit infoInitUnit)
    {
        _unitMain = unitMain;
        _unitType = infoInitUnit.unitType;
        _unitStat = LoadDataUnitClassStats.Instance.GetBaseStats(unitClass);
        _unitStat.SetStatCurrentLevel(infoInitUnit.level);
        UpdateBonusStat();
        _currentHp = MaxHp;
        unitMain.HpBar.SetHpBar(_currentHp, MaxHp);
    }

    public void OnUnitLevelUp(int level)
    {
        _unitStat.SetStatCurrentLevel(level);
        UpdateBonusStat();
        _currentHp = MaxHp;
        _unitMain.HpBar.SetHpBar(_currentHp, MaxHp);
    }

    public InfoDamage OnUnitAttack()
    {
        var finalAtk = _unitStat.CalDamage(_bonusStat);
        return new InfoDamage
        {
            attackerClass = unitClass,
            finalDamage = finalAtk
        };
    }

    public void OnUnitDamaged(InfoDamage infoDamage)
    {
        var finalDef = _unitStat.CalDefend(_bonusStat);
        var damage = unitClass.CalDamaged(finalDef, infoDamage);
        CustomDebug.SetMessage(
            $"Damage to {_unitType} : ({infoDamage.finalDamage} - {finalDef})x {unitClass.GetWeakness(infoDamage.attackerClass)}  = {damage}",
            Color.yellow);
        _currentHp -= damage;
        _currentHp = Mathf.Clamp(_currentHp, 0, MaxHp);
        _unitMain.HpBar.SetHpBar(_currentHp, MaxHp);
        UIGameplayController.instance.ShowDmg(_unitMain, damage);
        if (_currentHp <= 0)
        {
            _unitMain.OnUnitDie();
        }
    }

    public void OnUnitHealed(int heal)
    {
        var tmp = _currentHp + heal;
        tmp = Mathf.Clamp(tmp, 0, MaxHp);
        _currentHp = tmp;
        _unitMain.HpBar.SetHpBar(_currentHp, MaxHp);
        UIGameplayController.instance.ShowDmg(_unitMain, -heal);
    }

    public void UpdateBonusStat()
    {
        if (_unitType == UnitType.Hero)
        {
            _bonusStat = BuffCollector.instance.GetBuffBonus();
        }
        else
        {
            return;
        }
    }
}