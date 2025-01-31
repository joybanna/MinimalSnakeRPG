using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnUnit : MonoBehaviour
{
    [SerializeField] protected UnitType unitType;
    [SerializeField] protected SpawnController spawnController;
    [SerializeField] protected List<UnitMain> _units;

    [SerializeField] protected Transform parent;

    public void SpawnUnitClass(UnitClass unitClass)
    {
        var isBox = spawnController.GetSpawnBox(out var spawnBox);
        if (!isBox)
        {
            CustomDebug.SetMessage("Spawn Box is null", Color.red);
        }
        else
        {
            var prefab = LoadDataPrefabsSpawnable.Instance.GetPrefabUnit(unitType, unitClass);
            var hero = SpawnUnitMain(prefab, spawnBox);
            _units ??= new List<UnitMain>();
            _units.Add(hero);
        }
    }

    protected virtual UnitMain SpawnUnitMain(UnitMain prefab, Box box)
    {
        var unit = Instantiate(prefab, box.transform.position, Quaternion.identity);
        var dir = ExtensionSystems.GetRandomDirection();
        var info = new InfoInitUnit(unitType, dir, box, 1);
        unit.Init(info);
        if (parent) unit.transform.SetParent(parent);
        return unit;
    }

    public void ClearUnits()
    {
        if (_units == null) return;
        _units.Clear();
    }

    public bool GetUnitOnMap(Box box, out UnitMain unit)
    {
        unit = null;
        if (_units.IsEmptyCollection()) return false;
        CustomDebug.SetMessage($"GetUnitOnMap {box.Grid}", Color.yellow);
        for (var index = _units.Count - 1; index >= 0; index--)
        {
            var u = _units[index];
            if (u == unit) continue;
            if (!u.CurrentBox.Equals(box)) continue;
            unit = u;
            return true;
        }

        return false;
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