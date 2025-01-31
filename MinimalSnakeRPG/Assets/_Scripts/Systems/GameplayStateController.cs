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
    [SerializeField] private HistoryMove historyMove;
    [SerializeField] private PlayerHeroControl playerHeroControl;


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
        EnemyTurnController.instance.OnEnemyTurnStart();
    }

    public void OnEnemyTurnEnd()
    {
        CurrentState = GameplayState.PlayerTurn;
        headGroup.OnPlayerTurnStart();
        // Debug.Log("to Player Turn");
    }

    public void OnGameStart()
    {
        CurrentState = GameplayState.PlayerTurn;
        headGroup.OnPlayerTurnStart();
        // Debug.Log("Game Start");
    }

    public void OnGameEnd()
    {
        CurrentState = GameplayState.NullState;
        // Debug.Log("Game End");
    }
}