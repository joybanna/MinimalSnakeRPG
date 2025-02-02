using TMPro;
using UnityEngine;

public class UICardPlayerUnit : UIHeroCard
{
    [SerializeField] private UIHeroCard heroCard;
    [SerializeField] private UIValueBarBase healthBar;
    [SerializeField] private UIValueBarBase expBar;
    [SerializeField] private TMP_Text levelText;

    public void InitCard(UnitClass unitClass, int level, int health, int maxHealth, int exp, int maxExp)
    {
        heroCard.InitCard(unitClass, level);
        healthBar.SetValue(health, maxHealth);
        expBar.SetValue(exp, maxExp);
        levelText.text = $"{level}";
    }

    public void OnDamaged(int health, int maxHealth)
    {
        healthBar.SetValue(health, maxHealth);
    }

    public void OnHealed(int health, int maxHealth)
    {
        healthBar.SetValue(health, maxHealth);
    }

    public void OnLeveledUp(int level, int exp, int maxExp)
    {
        levelText.text = $"{level}";
        expBar.SetValue(exp, maxExp);
    }
}