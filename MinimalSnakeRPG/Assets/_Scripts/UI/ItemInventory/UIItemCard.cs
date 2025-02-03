using System;
using TMPro;
using UnityEngine;

public class UIItemCard : MonoBehaviour
{
    [SerializeField] private CollectibleType _collectibleType;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private UnityEngine.UI.Button _button;

    private CollectibleEffect _collectibleEffect;

    private void Awake()
    {
        _collectibleEffect = GetCollectibleEffect();
        if (_collectibleEffect == null)
        {
            this.gameObject.SetActive(false);
        }
    }

    private CollectibleEffect GetCollectibleEffect()
    {
        switch (_collectibleType)
        {
            case CollectibleType.Sword:
                return new SwordEffect();
            case CollectibleType.Shield:
                return new ShieldEffect();
            case CollectibleType.Potion:
                return new PotionEffect();
            default:
                return null;
        }
    }

    private void OnEnable()
    {
        InventoryManager.Instance.RegisterOnCountChanged(_collectibleType, UpdateCount);
        _button.onClick.AddListener(OnButtonClicked);
        UpdateCount();
    }

    private void OnDestroy()
    {
        InventoryManager.Instance.UnRegisterOnCountChanged(_collectibleType, UpdateCount);
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void UpdateCount()
    {
        var count = InventoryManager.Instance.GetCount(_collectibleType);
        _countText.text = count.ToString();
        _button.interactable = count > 0;
    }

    private void OnButtonClicked()
    {
        CustomDebug.SetMessage($"Use {_collectibleType}", Color.green);
        SoundController.instance.PlaySFX(SoundSource.UIClick);
        InventoryManager.Instance.RemoveItem(_collectibleType, 1);
        _collectibleEffect.ApplyEffect();
    }
}