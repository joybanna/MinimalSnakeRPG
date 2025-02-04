using UnityEngine;

public class VisualDir : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    private UnitMain _unitMain;

    public void Init(UnitMain unitMain)
    {
        _unitMain = unitMain;
    }

    public void ShowArrow(bool isShow)
    {
        arrow.SetActive(isShow);
    }
}