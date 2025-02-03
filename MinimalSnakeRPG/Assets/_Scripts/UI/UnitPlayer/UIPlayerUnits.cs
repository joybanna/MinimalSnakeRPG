using System.Collections.Generic;
using UnityEngine;

public class UIPlayerUnits : MonoBehaviour
{
    [SerializeField] private UICardPlayerUnit prefab;
    [SerializeField] private List<UICardPlayerUnit> playerUnits;
    [SerializeField] private RectTransform content;

    public UICardPlayerUnit InitHeadHero(UnitMain unit)
    {
        playerUnits ??= new List<UICardPlayerUnit>();
        var cardPlayerUnit = Instantiate(prefab, content);
        cardPlayerUnit.InitCard(unit);
        AddCardPlayerUnit(cardPlayerUnit);
        return cardPlayerUnit;
    }

    public UICardPlayerUnit RecruitedHero(UnitMain unit)
    {
        var cardPlayerUnit = Instantiate(prefab, content);
        cardPlayerUnit.InitCard(unit);
        AddCardPlayerUnit(cardPlayerUnit);
        return cardPlayerUnit;
    }

    private void AddCardPlayerUnit(UICardPlayerUnit cardPlayerUnit)
    {
        playerUnits.Add(cardPlayerUnit);
    }

    public void RemoveCardPlayerUnit(UICardPlayerUnit cardPlayerUnit)
    {
        playerUnits ??= new List<UICardPlayerUnit>();
        playerUnits.Remove(cardPlayerUnit);
    }

    public void SwapFirstToLast()
    {
        var first = playerUnits[0];
        playerUnits.RemoveAt(0);
        playerUnits.Add(first);
        Canvas.ForceUpdateCanvases();
    }

    public void SwapLastToFirst()
    {
        var last = playerUnits[^1];
        playerUnits.RemoveAt(playerUnits.Count - 1);
        playerUnits.Insert(0, last);
        Canvas.ForceUpdateCanvases();
    }

    public void RemoveCard(int index)
    {
        playerUnits[index].gameObject.SetActive(false);
        playerUnits.RemoveAt(index);
        Canvas.ForceUpdateCanvases();
    }
}