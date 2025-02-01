using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnController : MonoBehaviour
{
    public static EnemyTurnController instance;
    private List<EnemyAutoMove> _enemies;

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

    public void OnEnemyTurnStart()
    {
        // CustomDebug.SetMessage("Enemy Turn Start", Color.yellow);
        StartCoroutine(DelayedEnemyTurnEnd());
    }

    public void OnEnemyTurnEnd()
    {
        // CustomDebug.SetMessage("Enemy Turn End", Color.yellow);
        GameplayStateController.instance.OnEnemyTurnEnd();
    }

    private void MoveEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.MoveEnemy();
        }
    }

    IEnumerator DelayedEnemyTurnEnd()
    {
        foreach (var enemy in _enemies)
        {
            yield return new WaitForSeconds(0.5f);
            enemy.MoveEnemy();
        }
        yield return new WaitForSeconds(0.5f);
        OnEnemyTurnEnd();
    }

    public void InitEnemies(UnitMain unit)
    {
        var enemy = unit.GetComponent<EnemyAutoMove>();
        if (enemy == null) return;
        _enemies ??= new List<EnemyAutoMove>();
        _enemies.Add(enemy);
    }

    public void OnEnemyDie(UnitMain unit)
    {
        var enemy = unit.GetComponent<EnemyAutoMove>();
        if (enemy == null) return;
        _enemies.Remove(enemy);
    }
}