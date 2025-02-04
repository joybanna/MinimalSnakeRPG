using UnityEngine;

public class UISelectStartHero : MonoBehaviour
{
    [SerializeField] private RectTransform board;
    [SerializeField] private UICardSelectHero[] cards;
    [SerializeField] private UnitClass[] unitClasses;

    public void OpenPanel()
    {
        board.gameObject.SetActive(true);
        for (int i = 0; i < unitClasses.Length; i++)
        {
            cards[i].InitCard(this, unitClasses[i], 1);
        }
    }

    public void OnSelectedCard()
    {
        ClosePanel();
        
        // free items
        InventoryManager.Instance.AddItem(CollectibleType.Sword, 1);
        InventoryManager.Instance.AddItem(CollectibleType.Shield, 1);
        InventoryManager.Instance.AddItem(CollectibleType.Potion, 1);
        
        GameplayStateController.instance.OnGameStart();
    }

    public void ClosePanel()
    {
        board.gameObject.SetActive(false);
    }
}