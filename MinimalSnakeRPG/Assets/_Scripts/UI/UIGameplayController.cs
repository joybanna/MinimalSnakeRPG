using System;
using UnityEngine;

public class UIGameplayController : MonoBehaviour
{
    public static UIGameplayController instance;

    [SerializeField] private UISelectStartHero selectStartHero;

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