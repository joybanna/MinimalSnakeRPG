using TMPro;
using UnityEngine;

public class UICardPlayerUnit : UIHeroCard
{
    [SerializeField] private UIValueBarBase healthBar;
    [SerializeField] private UIExpUnit expBar;
    private InfoUnitClass _infoUnitClass;

    public void InitCard(UnitMain unit)
    {
        var uStats = unit.UnitStatus;
        var uLevel = unit.UnitLevelProgression;
        _infoUnitClass = LoadDataUnitClassStats.Instance.GetInfoUnitClass(uStats.UnitClass);
        this.InitCard(_infoUnitClass, uLevel.CurrentLevel);
        healthBar.SetValue(uStats.CurrentHp, uStats.MaxHp);
        SetLeveled(uLevel.CurrentLevel, uLevel.CurrentExp, uLevel.NextLevelExp);
        SetStats(uStats);
    }


    public void SetLeveled(int level, int exp, int maxExp)
    {
        CustomDebug.SetMessage($"SetLevel {level} : {exp} / {maxExp}", Color.green);
        expBar.SetLevel(level);
        expBar.SetValue(exp, maxExp);
    }

    public void UpdateCard(UnitMain unit)
    {
        InitCard(unit);
    }
}