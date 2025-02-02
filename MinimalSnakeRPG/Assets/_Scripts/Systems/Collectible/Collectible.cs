using UnityEngine;

public enum CollectibleType
{
    None = 0,
    Sword = 1,
    Shield = 2,
    Potion = 3,
}

public class Collectible : MonoBehaviour
{
    [SerializeField] protected CollectibleType collectibleType;
    public CollectibleType CollectibleType => collectibleType;

    public void OnCollectItem()
    {
        // remove collectible from map
        // play sound
        // remove collectible from collector
        SentToInventory();
    }

    private void SentToInventory()
    {
        InventoryManager.Instance.AddItem(collectibleType, 1);
    }
}