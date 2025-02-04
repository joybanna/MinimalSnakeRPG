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
        var isBonus = valueBonus > 0;
        if (isBonus)
        {
            var text = $" + {valueBonus}";
            statValue.text = valueMain + $"<color=#{ColorUtility.ToHtmlStringRGBA(Color.green)}>{text}</color>";
        }
        else
        {
            statValue.text = valueMain.ToString();
        }
    }
}