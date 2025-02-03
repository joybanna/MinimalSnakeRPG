using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectible : MonoBehaviour
{
    [SerializeField] protected SpawnController spawnController;
    [SerializeField] private Collectible prefabSword;
    [SerializeField] private Collectible prefabShield;
    [SerializeField] private Collectible prefabPotion;
    [SerializeField] private List<Collectible> _collectibles;
    [SerializeField] protected Transform parent;

    private readonly CollectibleType[] _collectibleTypes =
        { CollectibleType.Sword, CollectibleType.Shield, CollectibleType.Potion };

    private Dictionary<CollectibleType, Collectible> _collectiblePrefabs;

    private void Awake()
    {
        _collectiblePrefabs = new Dictionary<CollectibleType, Collectible>
        {
            { CollectibleType.Sword, prefabSword },
            { CollectibleType.Shield, prefabShield },
            { CollectibleType.Potion, prefabPotion }
        };
    }

    public void SpawnCollectibleItem(CollectibleType collectibleType)
    {
        var isBox = spawnController.GetSpawnBox(out var box);
        if (!isBox)
        {
            CustomDebug.SetMessage("Spawn Box is null", Color.red);
        }
        else
        {
            var prefab = _collectiblePrefabs[collectibleType];
            var collectible = Instantiate(prefab, box.transform.position, Quaternion.identity, parent);
            collectible.InitCollectible(this, collectibleType, box);
            box.BoxStatus = BoxStatus.Collectible;
            _collectibles ??= new List<Collectible>();
            _collectibles.Add(collectible);
        }
    }

    public void RandomSpawnCollectibleItem()
    {
        var randomType = _collectibleTypes[UnityEngine.Random.Range(0, _collectibleTypes.Length)];
        SpawnCollectibleItem(randomType);
    }

    public void RemoveCollectible(Collectible collectible)
    {
        if (_collectibles.IsEmptyCollection()) return;
        _collectibles.Remove(collectible);
    }
}

#if UNITY_EDITOR

[UnityEditor.CustomEditor(typeof(SpawnCollectible), true)]
public class SpawnCollectibleEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var spawnCollectible = (SpawnCollectible)target;
        if (GUILayout.Button("Random Spawn Collectible"))
        {
            spawnCollectible.RandomSpawnCollectibleItem();
        }

        if (GUILayout.Button("Spawn Sword"))
        {
            spawnCollectible.SpawnCollectibleItem(CollectibleType.Sword);
        }

        if (GUILayout.Button("Spawn Shield"))
        {
            spawnCollectible.SpawnCollectibleItem(CollectibleType.Shield);
        }

        if (GUILayout.Button("Spawn Potion"))
        {
            spawnCollectible.SpawnCollectibleItem(CollectibleType.Potion);
        }
    }
}

#endif