using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    private UnitType _unitType;
    [SerializeField] private UnitClass unitClass = UnitClass.Warrior;
    private UnitStat _unitStat;
    public UnitStat UnitStat => _unitStat;

    private UnitStat _bonusStat;
    public UnitStat BonusStat => _bonusStat;


    private int _currentHp;
    private int MaxHp => _unitStat.hp + _bonusStat.hp;
    [SerializeField] private UnitMain unitMain;

    public void Init(UnitType uType, int level)
    {
        _unitType = uType;
        _unitStat = LoadDataUnitClassStats.Instance.GetBaseStats(unitClass);
        _unitStat.SetStatCurrentLevel(level);
        _bonusStat = new UnitStat();
        _currentHp = MaxHp;
        unitMain.HpBar.SetHpBar(_currentHp, MaxHp);
    }

    public void OnUnitLevelUp(int level)
    {
        _unitStat.SetStatCurrentLevel(level);
        _currentHp = MaxHp;
        unitMain.HpBar.SetHpBar(_currentHp, MaxHp);
    }

    public InfoDamage OnUnitAttack()
    {
        var finalAtk = _unitStat.attack + _bonusStat.attack;
        return new InfoDamage
        {
            attackerClass = unitClass,
            finalDamage = finalAtk
        };
    }

    public void OnUnitDamaged(InfoDamage infoDamage)
    {
        var finalDef = _unitStat.defense + _bonusStat.defense;
        var damage = unitClass.CalDamaged(finalDef, infoDamage);
        CustomDebug.SetMessage($"Damage to {_unitType} : {damage}");
        _currentHp -= damage;
        unitMain.HpBar.SetHpBar(_currentHp, MaxHp);
        if (_currentHp <= 0)
        {
            unitMain.OnUnitDie();
        }
    }

    public void OnUnitHealed(int heal)
    {
        var tmp = _currentHp + heal;
        tmp = Mathf.Clamp(tmp, 0, MaxHp);
        _currentHp = tmp;
    }
}