using System;
using UnityEngine;

public enum GameplayState
{
    NullState = 0,
    PlayerTurn = 1,
    EnemyTurn = 2,
}

public class GameplayStateController : MonoBehaviour
{
    public static GameplayStateController instance;
    public GameplayState CurrentState { get; private set; }
    [SerializeField] private HeroHeadGroup headGroup;


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

        CurrentState = GameplayState.PlayerTurn;
    }

    public void OnPlayerTurnEnd()
    {
        CurrentState = GameplayState.EnemyTurn;
        GridBoxesCollector.instance.HideMoveableArea();
        Debug.Log("to Enemy Turn");
    }

    public void OnEnemyTurnEnd()
    {
        CurrentState = GameplayState.PlayerTurn;
        headGroup.OnPlayerTurnStart();
        Debug.Log("to Player Turn");
    }

    public void OnGameEnd()
    {
        CurrentState = GameplayState.NullState;
        Debug.Log("Game End");
    }
}