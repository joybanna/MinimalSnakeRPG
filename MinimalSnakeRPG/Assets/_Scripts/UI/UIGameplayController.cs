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
    [SerializeField] private UIShowInfoUnit showInfoUnit;
    [SerializeField] private UIDmgShowGroup dmgShowGroup;
    [SerializeField] private UIMenu menu;

    public UIMainController MainController => mainController;
    public UIPlayerUnits PlayerUnits => playerUnits;
    public UIBuffGroup BuffGroup => buffGroup;
    public UIEnemyTurn EnemyTurn => enemyTurn;
    public UIShowInfoUnit ShowInfoUnit => showInfoUnit;

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
        menu.OpenMenu(false);
    }

    public void OpenSelectStartHero()
    {
        SpawnController.instance.SpawnWave();
        InventoryManager.Instance.OnGameStart();
        selectStartHero.OpenPanel();
    }

    public void OpenGameOver()
    {
        PlayerHeroControl.instance.enabled = false;
        gameOver.OpenPanel();
    }

    public void ShowDmg(UnitMain unitMain, int dmg)
    {
        dmgShowGroup.ShowDmg(unitMain, dmg);
    }

    public void OnClickMenuInGame()
    {
        SoundController.instance.PlaySFX(SoundSource.UIClick);
        menu.OpenMenu(true);
        PlayerHeroControl.instance.IsControlEnable = false;
    }
}