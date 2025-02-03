using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBuffCard : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text turnCountText;
    [SerializeField] private RectTransform buffTurnCount;
    [SerializeField] private BuffRunner buffRunner;
    [SerializeField] private Sprite atkIcon;
    [SerializeField] private Sprite defIcon;
    private UIBuffGroup _buffGroup;

    public void InitCard(UIBuffGroup buffGroup, BuffRunner buff)
    {
        _buffGroup = buffGroup;
        this.buffRunner = buff;
        icon.sprite = buffRunner.Buff switch
        {
            Buff.Damage => atkIcon,
            Buff.Defense => defIcon,
            _ => null
        };
        turnCountText.text = buffRunner.TurnCount.ToString();
        buffRunner.onBuffTurnCountChange = OnTurnChange;
        buffRunner.onBuffRemove = OnRemoveBuff;
    }

    private void OnTurnChange(int turnCount)
    {
        turnCountText.text = turnCount.ToString();
    }

    private void OnRemoveBuff()
    {
        _buffGroup.RemoveBuff(this);
        Destroy(this.gameObject);
    }
}