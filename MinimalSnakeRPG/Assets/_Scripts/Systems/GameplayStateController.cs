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


    public int TurnCount { get; private set; }

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
        TurnCount = 0;
    }


    public void OnPlayerTurnEnd()
    {
        CurrentState = GameplayState.EnemyTurn;
        UIGameplayController.instance.EnemyTurn.OpenPanel();
        GridBoxesCollector.instance.HideMoveableArea();
        EnemyTurnController.instance.OnEnemyTurnStart();
    }

    public void OnEnemyTurnEnd()
    {
        CurrentState = GameplayState.PlayerTurn;
        headGroup.OnPlayerTurnStart();
        TurnCount++;
        BuffCollector.instance.OnTurnChange();
        UIGameplayController.instance.MainController.SetTurnCountText(TurnCount);
        UIGameplayController.instance.EnemyTurn.ClosePanel();
    }

    public void OnGameStart()
    {
        CurrentState = GameplayState.PlayerTurn;
        headGroup.OnPlayerTurnStart();
        TurnCount = 1;
        UIGameplayController.instance.MainController.SetTurnCountText(TurnCount);
    }

    public void OnGameEnd()
    {
        CurrentState = GameplayState.NullState;
        UIGameplayController.instance.OpenGameOver();
    }
}