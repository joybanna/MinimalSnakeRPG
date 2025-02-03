using System;
using UnityEngine;

public class UIGameplayController : MonoBehaviour
{
    public static UIGameplayController instance;

    [SerializeField] private UIMainController mainController;
    [SerializeField] private UISelectStartHero selectStartHero;
    [SerializeField] private UIPlayerUnits playerUnits;
    [SerializeField] private UIBuffGroup buffGroup;
    [SerializeField] private UIEnemyTurn enemyTurn;
    [SerializeField] private UIGameOver gameOver;

    public UIMainController MainController => mainController;
    public UIPlayerUnits PlayerUnits => playerUnits;
    public UIBuffGroup BuffGroup => buffGroup;
    public UIEnemyTurn EnemyTurn => enemyTurn;

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

    private void Start()
    {
        SpawnController.instance.SpawnWave();
        OpenSelectStartHero();
    }

    public void OpenSelectStartHero()
    {
        selectStartHero.OpenPanel();
    }

    public void OpenGameOver()
    {
        gameOver.OpenPanel();
    }
}