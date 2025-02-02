using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StatType
{
    None = 0,
    Attack = 1,
    Defense = 2,
    Health = 3,
}

public class UIUnitStatCard : MonoBehaviour
{
    [SerializeField] private StatType statType;
    [SerializeField] private TMP_Text statValue;
    public StatType StatType => statType;

    public void SetStatValue(int valueMain, int valueBonus)
    {
        statValue.text = valueMain + " + " + valueBonus;
    }
}