using UnityEngine;

public class UIShowInfoUnit : MonoBehaviour
{
    [SerializeField] private UICardPlayerUnit infoCard;

    private UnitMain _currentUnit;

    public UIShowInfoUnit ShowInfo(UnitMain unit)
    {
        _currentUnit = unit;
        infoCard.InitCard(unit);
        infoCard.gameObject.SetActive(true);
        return this;
    }

    public void HideCard(UnitMain unit)
    {
        if (unit == _currentUnit || unit == null)
        {
            infoCard.gameObject.SetActive(false);
        }
    }
    
    public void UpdateCard(UnitMain unit)
    {
        if (unit == _currentUnit)
        {
            infoCard.UpdateCard(_currentUnit);
        }
       
    }
}