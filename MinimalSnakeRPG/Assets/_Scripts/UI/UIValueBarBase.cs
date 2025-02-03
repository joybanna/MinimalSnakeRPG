using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIValueBarBase : MonoBehaviour
{
    [SerializeField] protected Image progressBar;
    [SerializeField] protected TMP_Text valueText;

    public virtual void SetValue(int value, int maxValue)
    {
        progressBar.fillAmount = value / (float)maxValue;
        if (valueText) valueText.text = $"{value}/{maxValue}";
    }
}