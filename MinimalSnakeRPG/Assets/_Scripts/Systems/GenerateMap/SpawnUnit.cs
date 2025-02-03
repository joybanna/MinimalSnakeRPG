using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnUnit : MonoBehaviour
{
    [SerializeField] protected UnitType unitType;
    [SerializeField] protected SpawnController spawnController;
    [SerializeField] protected List<UnitMain> _units;

    [SerializeField] protected Transform parent;

    public void SpawnUnitClass(UnitClass unitClass, int level = 1)
    {
        var isBox = spawnController.GetSpawnBox(out var spawnBox);
        if (!isBox)
        {
            CustomDebug.SetMessage("Spawn Box is null", Color.red);
        }
        else
        {
            var prefab = LoadDataPrefabsSpawnable.Instance.GetPrefabUnit(unitType, unitClass);
            var hero = SpawnUnitMain(prefab, spawnBox, level);
            _units ??= new List<UnitMain>();
            _units.Add(hero);
        }
    }

    public void SpawnUnitRandomClass()
    {
        var classUnit = CalculateStats.RandomUnitClass();
        SpawnUnitClass(classUnit);
    }

    protected virtual UnitMain SpawnUnitMain(UnitMain prefab, Box box, int level = 1)
    {
        var unit = Instantiate(prefab, box.transform.position, Quaternion.identity);
        var dir = ExtensionSystems.GetRandomDirection();
        var info = new InfoInitUnit(unitType, dir, box, level);
        unit.Init(info);
        if (parent) unit.transform.SetParent(parent);

        if (unitType == UnitType.Enemy)
        {
            var enemyAutoMove = unit.GetComponent<EnemyAutoMove>();
            enemyAutoMove.InitEnemy(unit, CalculateSpawnMap.GetEnemyMoveable());
        }

        return unit;
    }

    public void ClearUnits()
    {
        if (_units == null) return;
        _units.Clear();
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(SpawnUnit), true)]
public class SpawnUnitEditor : UnityEditor.Editor
{
    private SpawnUnit _spawnUnit;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _spawnUnit = (SpawnUnit)target;
        if (GUILayout.Button("Spawn Warrior"))
        {
            _spawnUnit.SpawnUnitClass(UnitClass.Warrior);
        }

        if (GUILayout.Button("Spawn Rogue"))
        {
            _spawnUnit.SpawnUnitClass(UnitClass.Rogue);
        }

        if (GUILayout.Button("Spawn Wizard"))
        {
            _spawnUnit.SpawnUnitClass(UnitClass.Wizard);
        }

        if (GUILayout.Button("Clear Units"))
        {
            _spawnUnit.ClearUnits();
        }
    }
}
#endif