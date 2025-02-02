using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIHeroCard : MonoBehaviour
{
    [SerializeField] protected TMP_Text heroName;
    [SerializeField] protected Image heroImage;
    [SerializeField] protected UIUnitStatsCards statsCards;

    public virtual void InitCard(UnitClass unitClass, int level)
    {
        heroName.text = unitClass.ToString();
        // heroImage.sprite = unitClass.UnitSprite;
        statsCards.InitStatsCards(unitClass, level);
    }
}