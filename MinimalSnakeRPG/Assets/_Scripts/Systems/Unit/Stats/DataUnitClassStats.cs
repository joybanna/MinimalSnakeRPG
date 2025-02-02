using UnityEngine;

[CreateAssetMenu(fileName = "DataUnitClassStats", menuName = "GameData/DataUnitClassStats")]
public class DataUnitClassStats : ScriptableObject
{
    [SerializeField] private UnitStat baseStatsWarrior;
    [SerializeField] private UnitStat baseStatsRogue;
    [SerializeField] private UnitStat baseStatsWizard;

    [SerializeField] private Sprite iconWarrior;
    [SerializeField] private Sprite iconRogue;
    [SerializeField] private Sprite iconWizard;

    [SerializeField] private string warriorAbility;
    [SerializeField] private string rogueAbility;
    [SerializeField] private string wizardAbility;

    [SerializeField] private string warriorPassive;
    [SerializeField] private string roguePassive;
    [SerializeField] private string wizardPassive;

    public string GetPassive(UnitClass unitType)
    {
        switch (unitType)
        {
            case UnitClass.Warrior:
                return warriorPassive;
            case UnitClass.Rogue:
                return roguePassive;
            case UnitClass.Wizard:
                return wizardPassive;
            default:
                return warriorPassive;
        }
    }

    public string GetAbility(UnitClass unitType)
    {
        switch (unitType)
        {
            case UnitClass.Warrior:
                return warriorAbility;
            case UnitClass.Rogue:
                return rogueAbility;
            case UnitClass.Wizard:
                return wizardAbility;
            default:
                return warriorAbility;
        }
    }

    public Sprite GetIcon(UnitClass unitType)
    {
        switch (unitType)
        {
            case UnitClass.Warrior:
                return iconWarrior;
            case UnitClass.Rogue:
                return iconRogue;
            case UnitClass.Wizard:
                return iconWizard;
            default:
                return iconWarrior;
        }
    }

    public UnitStat GetBaseStats(UnitClass unitType)
    {
        switch (unitType)
        {
            case UnitClass.Warrior:
                return baseStatsWarrior;
            case UnitClass.Rogue:
                return baseStatsRogue;
            case UnitClass.Wizard:
                return baseStatsWizard;
            default:
                return baseStatsWarrior;
        }
    }
}

public class LoadDataUnitClassStats : Singleton<LoadDataUnitClassStats>
{
    private DataUnitClassStats _dataUnitClassStats;
    private const string PATH = "DataUnitClassStats";

    public LoadDataUnitClassStats()
    {
        _dataUnitClassStats = Resources.Load<DataUnitClassStats>(PATH);
        if (_dataUnitClassStats == null)
        {
            CustomDebug.SetMessage("DataUnitClassStats is null", Color.red);
        }
    }

    public UnitStat GetBaseStats(UnitClass uClass)
    {
        return _dataUnitClassStats.GetBaseStats(uClass);
    }

    public InfoUnitClass GetInfoUnitClass(UnitClass uClass)
    {
        var icon = _dataUnitClassStats.GetIcon(uClass);
        var ability = _dataUnitClassStats.GetAbility(uClass);
        var passive = _dataUnitClassStats.GetPassive(uClass);
        return new InfoUnitClass
        {
            unitClass = uClass,
            icon = icon,
            ability = ability,
            passive = passive
        };
    }
}

public struct InfoUnitClass
{
    public UnitClass unitClass;
    public Sprite icon;
    public string ability;
    public string passive;
}