using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataPrefabsSpawnable", menuName = "GameData/DataPrefabsSpawnable")]
public class DataPrefabsSpawnable : ScriptableObject
{
    [SerializeField] private Box boxGridPrefab;
    public Box BoxGridPrefab => boxGridPrefab;

    [SerializeField] private UnitMain heroWarriorPrefab;
    [SerializeField] private UnitMain heroRoguePrefab;
    [SerializeField] private UnitMain heroWizardPrefab;

    [SerializeField] private UnitMain enemyWarriorPrefab;
    [SerializeField] private UnitMain enemyRoguePrefab;
    [SerializeField] private UnitMain enemyWizardPrefab;

    public Dictionary<UnitClass, UnitMain> GetUnitPrefab(UnitType unitType)
    {
        var unitPrefabs = new Dictionary<UnitClass, UnitMain>();
        switch (unitType)
        {
            case UnitType.Hero:
                unitPrefabs.Add(UnitClass.Warrior, heroWarriorPrefab);
                unitPrefabs.Add(UnitClass.Rogue, heroRoguePrefab);
                unitPrefabs.Add(UnitClass.Wizard, heroWizardPrefab);
                break;
            case UnitType.Enemy:
                unitPrefabs.Add(UnitClass.Warrior, enemyWarriorPrefab);
                unitPrefabs.Add(UnitClass.Rogue, enemyRoguePrefab);
                unitPrefabs.Add(UnitClass.Wizard, enemyWizardPrefab);
                break;
            default:
                break;
        }

        return unitPrefabs;
    }
}

public class LoadDataPrefabsSpawnable : Singleton<LoadDataPrefabsSpawnable>
{
    private DataPrefabsSpawnable _dataPrefabsSpawnable;
    private const string PATH = "DataPrefabsSpawnable";
    private Box _boxGridPrefab;
    public Box BoxGridPrefab => _boxGridPrefab;

    private Dictionary<UnitClass, UnitMain> _unitHeroPrefabs;
    public Dictionary<UnitClass, UnitMain> UnitHeroPrefabs => _unitHeroPrefabs;

    private Dictionary<UnitClass, UnitMain> _unitEnemyPrefabs;
    public Dictionary<UnitClass, UnitMain> UnitEnemyPrefabs => _unitEnemyPrefabs;

    public LoadDataPrefabsSpawnable()
    {
        _dataPrefabsSpawnable = Resources.Load<DataPrefabsSpawnable>(PATH);
        if (_dataPrefabsSpawnable == null)
        {
            CustomDebug.SetMessage("DataPrefabsSpawnable is null", Color.red);
        }
        else
        {
            _boxGridPrefab = _dataPrefabsSpawnable.BoxGridPrefab;
            _unitHeroPrefabs = _dataPrefabsSpawnable.GetUnitPrefab(UnitType.Hero);
            _unitEnemyPrefabs = _dataPrefabsSpawnable.GetUnitPrefab(UnitType.Enemy);
        }
    }

    public UnitMain GetPrefabUnit(UnitType unitType, UnitClass unitClass)
    {
        var list = unitType == UnitType.Hero ? _unitHeroPrefabs : _unitEnemyPrefabs;
        var isCon = list.TryGetValue(unitClass, out var unitMain);
        if (isCon)
        {
            return unitMain;
        }
        else
        {
            CustomDebug.SetMessage($"UnitMain is null  {unitType} , {unitClass}", Color.red);
            return null;
        }
    }
}