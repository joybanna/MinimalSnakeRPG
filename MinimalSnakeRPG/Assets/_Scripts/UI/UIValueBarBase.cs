using UnityEngine;
using UnityEngine.UI;

public class UIValueBarBase : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private Text valueText;

    public void SetValue(int value, int maxValue)
    {
        progressBar.fillAmount = value / (float)maxValue;
        if (valueText) valueText.text = $"{value}/{maxValue}";
    }
}