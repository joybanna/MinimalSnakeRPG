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
    private SpawnCollectible _spawner;

    public void InitCollectible(SpawnCollectible spawner, CollectibleType type, Box box)
    {
        _spawner = spawner;
        collectibleType = type;
        box.BoxStatus = BoxStatus.Collectible;
    }

    public void OnCollectItem()
    {
        CustomDebug.SetMessage($"Collect - {collectibleType}", Color.green);
        _spawner.RemoveCollectible(this);
        SentToInventory();
        Destroy(this.gameObject);
    }

    private void SentToInventory()
    {
        InventoryManager.Instance.AddItem(collectibleType, 1);
    }
}