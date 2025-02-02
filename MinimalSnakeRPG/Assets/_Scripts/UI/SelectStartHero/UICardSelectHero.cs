using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICardSelectHero : MonoBehaviour
{
    [SerializeField] private UIHeroCard heroCard;
    [SerializeField] private TMP_Text heroAbility;
    [SerializeField] private TMP_Text heroPassive;
    private InfoUnitClass _infoUnitClass;
    private UISelectStartHero _main;

    public void InitCard(UISelectStartHero main, UnitClass unitClass, int level)
    {
        _main = main;
        _infoUnitClass = LoadDataUnitClassStats.Instance.GetInfoUnitClass(unitClass);
        heroCard.InitCard(unitClass, level);
        heroAbility.text = $"Ability : " + _infoUnitClass.ability;
        heroPassive.text = $"Passive : " + _infoUnitClass.passive;
    }

    public void OnSelectCard()
    {
        SpawnController.instance.SpawnStarter(_infoUnitClass.unitClass, 1);
        _main.OnSelectedCard();
    }
}