using System;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

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
    }
}