using UnityEngine;

public class UIEnemyTurn : MonoBehaviour
{
    [SerializeField] protected RectTransform panel;

    public void OpenPanel()
    {
        panel.gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.gameObject.SetActive(false);
    }
}