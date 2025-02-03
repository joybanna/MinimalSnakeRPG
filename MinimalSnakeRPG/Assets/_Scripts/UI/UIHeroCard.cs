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

    public virtual void InitCard(InfoUnitClass infoUnitClass, int level)
    {
        heroName.text = infoUnitClass.unitClass.ToString();
        heroImage.sprite = infoUnitClass.icon;
        statsCards.InitStatsCards(infoUnitClass.unitClass, level);
    }

    public virtual void SetStats(UnitStatus uStats)
    {
        statsCards.UpdateStatsCards(uStats);
    }
}