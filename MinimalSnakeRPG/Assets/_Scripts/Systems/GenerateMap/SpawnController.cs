using System;
using System.Collections;
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

    [SerializeField] private int wave;

    public void SpawnWave()
    {
        StartCoroutine(ChainSpawnUnits());
    }


    public void NextWave()
    {
        wave++;
        CustomDebug.SetMessage("NextWave: " + wave, Color.green);
        SpawnWave();
    }

    private IEnumerator ChainSpawnUnits()
    {
        yield return SpawnUnits(UnitType.Enemy);
        yield return SpawnUnits(UnitType.Hero);
    }

    private IEnumerator SpawnUnits(UnitType unitType)
    {
        var count = CalculateSpawnMap.GetContSpawnUnit(unitType, wave);
        if (count == 0) yield break;
        var selected = unitType == UnitType.Hero ? spawnHero : spawnEnemy;
        for (int i = 0; i < count; i++)
        {
            selected.SpawnUnitRandomClass();
            yield return new WaitForSeconds(0.1f);
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
        base.OnInspectorGUI();
        _spawnController = (SpawnController)target;

        if (GUILayout.Button("Shuffle Boxes"))
        {
            _spawnController.ShuffleListBoxes();
        }

        if (GUILayout.Button("Spawn Wave"))
        {
            _spawnController.SpawnWave();
        }
    }
}

#endif