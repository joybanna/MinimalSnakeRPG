using UnityEngine;

[CreateAssetMenu(fileName = "DataUnitClassStats", menuName = "GameData/DataUnitClassStats")]
public class DataUnitClassStats : ScriptableObject
{
    [SerializeField] private UnitStat baseStatsWarrior;
    [SerializeField] private UnitStat baseStatsRogue;
    [SerializeField] private UnitStat baseStatsWizard;

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
}