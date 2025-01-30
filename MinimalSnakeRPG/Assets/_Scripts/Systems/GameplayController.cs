using System;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField] private HeroHeadGroup headGroup;

    [SerializeField] private Box startBox;

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

    public void Start()
    {
        headGroup.InitHero(startBox, UnitDirection.Up);

        headGroup.OnPlayerTurnStart();
    }
}