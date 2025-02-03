using System.Collections.Generic;
using UnityEngine;

public class UIBuffGroup : MonoBehaviour
{
    [SerializeField] private UIBuffCard prefab;
    [SerializeField] private List<UIBuffCard> buffCards;
    [SerializeField] private RectTransform content;

    private void Awake()
    {
        buffCards = new List<UIBuffCard>();
    }

    public UIBuffCard AddBuff(BuffRunner buff)
    {
        var buffCard = Instantiate(prefab, content);
        buffCard.InitCard(this, buff);
        buffCards.Add(buffCard);
        return buffCard;
    }

    public void RemoveBuff(UIBuffCard buffCard)
    {
        buffCards.Remove(buffCard);
    }
}