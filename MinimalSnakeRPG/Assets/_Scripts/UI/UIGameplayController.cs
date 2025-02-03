using System;
using UnityEngine;

public class UIGameplayController : MonoBehaviour
{
    public static UIGameplayController instance;

    [SerializeField] private UIMainController mainController;
    [SerializeField] private UISelectStartHero selectStartHero;
    [SerializeField] private UIPlayerUnits playerUnits;
    [SerializeField] private UIBuffGroup buffGroup;

    public UIMainController MainController => mainController;
    public UIPlayerUnits PlayerUnits => playerUnits;
    public UIBuffGroup BuffGroup => buffGroup;

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
}