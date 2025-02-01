using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitsCollector : MonoBehaviour
{
    public static UnitsCollector instance;

    [SerializeField] private List<UnitMain> heroMovements;
    [SerializeField] private List<UnitMain> enemies;

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
    }

    public void OnUnitEntry(UnitType uType, UnitMain unit)
    {
        if (uType == UnitType.Hero)
        {
            heroMovements ??= new List<UnitMain>();
            heroMovements.Add(unit);
        }
        else if (uType == UnitType.Enemy)
        {
            enemies ??= new List<UnitMain>();
            enemies.Add(unit);
            EnemyTurnController.instance.InitEnemies(unit);
        }
    }

    public void OnUnitExit(UnitType uType, UnitMain unit)
    {
        if (uType == UnitType.Hero)
        {
            heroMovements.Remove(unit);
        }
        else if (uType == UnitType.Enemy)
        {
            enemies.Remove(unit);
            EnemyTurnController.instance.OnEnemyDie(unit);
            if (enemies.Count == 0)
            {
                GameplayController.instance.OnEnemiesWaveDie();
            }
        }
    }

    public bool GetUnitDamaged(UnitType defender, Box box, out UnitMain unit)
    {
        unit = null;
        switch (defender)
        {
            case UnitType.Hero when heroMovements.GetUnitOnMap(box, out unit):
            case UnitType.Enemy when enemies.GetUnitOnMap(box, out unit):
                return true;
            default:
                return false;
        }
    }
}