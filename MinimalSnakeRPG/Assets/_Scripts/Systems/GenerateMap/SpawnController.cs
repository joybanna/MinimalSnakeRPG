using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static SpawnController instance;
    [SerializeField] private GridBoxesCollector gridBoxesCollector;
    [SerializeField] private List<Box> _randomBoxes;
    [SerializeField] private SpawnUnit spawnHero;
    [SerializeField] private SpawnUnit spawnEnemy;
    [SerializeField] private SpawnStarterUnit spawnStarterUnit;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ShuffleListBoxes();
    }

    public void ShuffleListBoxes()
    {
        _randomBoxes = gridBoxesCollector.GridBoxes.Shuffle();
    }


    public bool GetSpawnBox(out Box box)
    {
        if (_randomBoxes.Count == 0)
        {
            CustomDebug.SetMessage("Random Boxes is empty", Color.red);
            box = null;
            return false;
        }

        for (int i = _randomBoxes.Count - 1; i >= 0; i--)
        {
            var b = _randomBoxes[i];
            if (b.IsBorderBox()) continue;
            if (b.BoxStatus != BoxStatus.Empty) continue;
            box = b;
            return true;
        }

        box = null;
        return false;
    }

    public bool GetUnitDamaged(UnitType defender, Box box, out UnitMain unit)
    {
        unit = null;
        switch (defender)
        {
            case UnitType.Hero when spawnHero.GetUnitOnMap(box, out unit):
            case UnitType.Enemy when spawnEnemy.GetUnitOnMap(box, out unit):
                return true;
            default:
                return false;
        }
    }
}

#if UNITY_EDITOR

[UnityEditor.CustomEditor(typeof(SpawnController), true)]
public class SpawnControllerEditor : UnityEditor.Editor
{
    private SpawnController _spawnController;


    public override void OnInspectorGUI()
    {
        _spawnController = (SpawnController)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Shuffle Boxes"))
        {
            _spawnController.ShuffleListBoxes();
        }
    }
}

#endif