using System.Collections;
using UnityEngine;

public class EnemyTurnController : MonoBehaviour
{
    public static EnemyTurnController instance;

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


    IEnumerator DelayedEnemyTurnEnd()
    {
        yield return new WaitForSeconds(0.5f);
        OnEnemyTurnEnd();
    }
}