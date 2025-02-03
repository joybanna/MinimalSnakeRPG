using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : Singleton<InventoryManager>
{
    private ModuleItem _sword;
    private ModuleItem _shield;
    private ModuleItem _potion;

    private Dictionary<CollectibleType, ModuleItem> _inventory;

    public InventoryManager()
    {
        _sword = new ModuleItem(CollectibleType.Sword);
        _shield = new ModuleItem(CollectibleType.Shield);
        _potion = new ModuleItem(CollectibleType.Potion);
        _inventory = new Dictionary<CollectibleType, ModuleItem>
        {
            { CollectibleType.Sword, _sword },
            { CollectibleType.Shield, _shield },
            { CollectibleType.Potion, _potion }
        };
    }

    public void AddItem(CollectibleType collectibleType, int count)
    {
        var isItem = _inventory.TryGetValue(collectibleType, out var item);
        if (isItem)
        {
            item.AddItem(count);
        }
    }


    public void RemoveItem(CollectibleType collectibleType, int count)
    {
        var isItem = _inventory.TryGetValue(collectibleType, out var item);
        if (isItem && item.IsRemovable(count))
        {
            item.RemoveItem(count);
        }
    }

    public bool IsRemovable(CollectibleType collectibleType, int count)
    {
        var isItem = _inventory.TryGetValue(collectibleType, out var item);
        return isItem && item.IsRemovable(count);
    }

    public void RegisterOnCountChanged(CollectibleType collectibleType, UnityAction action)
    {
        var isItem = _inventory.TryGetValue(collectibleType, out var item);
        if (isItem)
        {
            item.RegisterOnCountChanged(action);
        }
    }

    public void UnRegisterOnCountChanged(CollectibleType collectibleType, UnityAction action)
    {
        var isItem = _inventory.TryGetValue(collectibleType, out var item);
        if (isItem)
        {
            item.UnRegisterOnCountChanged(action);
        }
    }

    public int GetCount(CollectibleType collectibleType)
    {
        var isItem = _inventory.TryGetValue(collectibleType, out var item);
        return isItem ? item.Count : 0;
    }
}

public class ModuleItem
{
    public CollectibleType CollectibleType { get; private set; }
    public int Count { get; private set; }

    private UnityAction _onCountChanged;

    public ModuleItem(CollectibleType collectibleType)
    {
        CollectibleType = collectibleType;
        Count = 0;
    }

    public void AddItem(int add)
    {
        Count += add;
        _onCountChanged?.Invoke();
    }

    public void RemoveItem(int remove)
    {
        Count -= remove;
        Count = Mathf.Max(0, Count);
        _onCountChanged?.Invoke();
    }

    public bool IsRemovable(int remove)
    {
        return Count >= remove;
    }

    public void RegisterOnCountChanged(UnityAction action)
    {
        _onCountChanged += action;
    }

    public void UnRegisterOnCountChanged(UnityAction action)
    {
        _onCountChanged -= action;
    }
}