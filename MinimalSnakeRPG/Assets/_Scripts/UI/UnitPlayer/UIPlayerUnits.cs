using System.Collections.Generic;
using UnityEngine;

public class UIPlayerUnits : MonoBehaviour
{
    [SerializeField] private UICardPlayerUnit prefab;
    [SerializeField] private List<UICardPlayerUnit> playerUnits;
    [SerializeField] private RectTransform content;
    private Dictionary<UnitMain, UICardPlayerUnit> _playerUnits;

    public UICardPlayerUnit InitHeadHero(UnitMain unit)
    {
        playerUnits ??= new List<UICardPlayerUnit>();
        _playerUnits ??= new Dictionary<UnitMain, UICardPlayerUnit>();
        var cardPlayerUnit = Instantiate(prefab, content);
        cardPlayerUnit.InitCard(unit);
        AddCardPlayerUnit(unit, cardPlayerUnit);
        return cardPlayerUnit;
    }

    public UICardPlayerUnit RecruitedHero(UnitMain unit)
    {
        var cardPlayerUnit = Instantiate(prefab, content);
        cardPlayerUnit.InitCard(unit);
        AddCardPlayerUnit(unit, cardPlayerUnit);
        return cardPlayerUnit;
    }

    private void AddCardPlayerUnit(UnitMain unit, UICardPlayerUnit cardPlayerUnit)
    {
        playerUnits.Add(cardPlayerUnit);
        _playerUnits.Add(unit, cardPlayerUnit);
    }

    public void RemoveCardPlayerUnit(UnitMain unit)
    {
        var isFind = _playerUnits.TryGetValue(unit, out var cardPlayerUnit);
        if (!isFind) return;
        Destroy(cardPlayerUnit.gameObject);
        playerUnits.Remove(cardPlayerUnit);
        _playerUnits.Remove(unit);
        Canvas.ForceUpdateCanvases();
    }

    public void SwapFirstToLast()
    {
        var first = playerUnits[0];
        first.transform.SetAsLastSibling();
        Canvas.ForceUpdateCanvases();
    }

    public void SwapLastToFirst()
    {
        var last = playerUnits[^1];
        last.transform.SetAsFirstSibling();
        Canvas.ForceUpdateCanvases();
    }


    public void UpdatePlayerUnit(UnitMain unitMain) // buff , level up
    {
        if (unitMain != null)
        {
            var isCon = _playerUnits.TryGetValue(unitMain, out var card);
            if (isCon)
            {
                card.UpdateCard(unitMain);
            }
        }
        else
        {
            foreach (var unit in _playerUnits)
            {
                unit.Value.UpdateCard(unit.Key);
            }
        }
    }
}