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
    [SerializeField] private SpawnCollectible spawnCollectible;
    [SerializeField] private SpawnObstacle spawnObstacle;

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


    public bool GetSpawnBox(bool isReRandom, out Box box)
    {
        if (_randomBoxes.Count == 0)
        {
            CustomDebug.SetMessage("Random Boxes is empty", Color.red);
            box = null;
            return false;
        }

        if (isReRandom) ShuffleListBoxes();
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
        StartCoroutine(ChainSpawnUnits(true));
        UIGameplayController.instance.MainController.SetEnemyWaveText(wave);
    }


    public void NextWave()
    {
        wave++;
        UIGameplayController.instance.MainController.SetEnemyWaveText(wave);
        CustomDebug.SetMessage("NextWave: " + wave, Color.green);
        StartCoroutine(ChainSpawnUnits(false));
    }

    private IEnumerator ChainSpawnUnits(bool isStart = false)
    {
        yield return spawnObstacle.Spawns(wave);
        yield return spawnEnemy.Spawns(wave);
        yield return spawnHero.Spawns(wave);
        yield return spawnCollectible.Spawns(wave);
        if (!isStart)
        {
            GameplayStateController.instance.ContinueStateToEnemy();
        }
    }

    public void SpawnStarter(UnitClass uClass, int level)
    {
        spawnStarterUnit.SpawnUnitClass(uClass, wave);
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