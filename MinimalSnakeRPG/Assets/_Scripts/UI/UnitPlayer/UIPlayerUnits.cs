using System.Collections.Generic;
using UnityEngine;

public class UIPlayerUnits : MonoBehaviour
{
    [SerializeField] private UICardPlayerUnit card;
    private UnitMain _headUnit;


    public UICardPlayerUnit InitHeadHero(UnitMain unit)
    {
        _headUnit = unit;
        card.InitCard(unit);
        card.gameObject.SetActive(true);
        return card;
    }


    public void UpdatePlayerUnit(UnitMain unitMain) // buff , level up
    {
        if (_headUnit == unitMain)
        {
            card.UpdateCard(unitMain);
        }
    }
}