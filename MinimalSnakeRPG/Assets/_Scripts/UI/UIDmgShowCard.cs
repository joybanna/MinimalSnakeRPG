using TMPro;
using UnityEngine;

public class UIDmgShowCard : MonoBehaviour
{
    [SerializeField] private RectTransform board;
    [SerializeField] private TMP_Text dmgText;

    private Camera _camera;

    public void Init(Camera c)
    {
        board.gameObject.SetActive(false);
        _camera = c;
    }

    public void ShowDmg(UnitMain unitMain, int dmg)
    {
        var isDmg = dmg >= 0;
        board.gameObject.SetActive(true);
        var num = Mathf.Abs(dmg);
        dmgText.color = isDmg ? Color.red : Color.green;
        dmgText.text = isDmg ? $"-{num}" : $"+{num}";

        var pos = unitMain.transform.position;
        pos.y += 0.2f;
        board.position = _camera.WorldToScreenPoint(pos);

        Invoke(nameof(HideCard), 0.7f);
    }

    private void HideCard()
    {
        board.gameObject.SetActive(false);
    }
}