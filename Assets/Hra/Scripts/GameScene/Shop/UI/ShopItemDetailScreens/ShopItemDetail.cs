using System;
using UnityEngine;
using TMPro;

public class ShopItemDetail : MonoBehaviour
{
    [SerializeField] private WeaponDetail _weaponDetail;
    [SerializeField] private ArmorDetail _armorDetail;
    [SerializeField] private BuffDetail _buffDetail;
    [SerializeField] private ConsumableDetail _consumableDetail;

    [SerializeField] private TMP_Text _buyButtonText;

    private ItemInstance _currentInstance;
    private ItemDetail _currentDetail;

    public event Action<ItemInstance> OnItemPurchased;
    public event Action OnDetailClosed;

    public void Init(ItemInstance itemInstance)
    {
        _currentInstance = itemInstance;
        _buyButtonText.text = "Buy: " + (itemInstance.DiscountedPrice != 0 ? 
            itemInstance.DiscountedPrice : itemInstance.Price).ToString();
        HideCurrentDetail();

        switch (itemInstance)
        {
            case WeaponInstance:
                _currentDetail = Instantiate(_weaponDetail, transform);
                break;
            case ArmorInstance:
                _currentDetail = Instantiate(_armorDetail, transform);
                break;
            case BuffInstance:
                _currentDetail = Instantiate(_buffDetail, transform);
                break;
            case ConsumableInstance:
                _currentDetail = Instantiate(_consumableDetail, transform);
                break;
        }

        _currentDetail.Init(itemInstance);
    }

    private void HideCurrentDetail()
    {
        if (_currentDetail != null)
        {
            Destroy(_currentDetail.gameObject);
        }
    }

    public void Hide()
    {
        OnDetailClosed?.Invoke();
    }

    public void Buy()
    {
        OnItemPurchased?.Invoke(_currentInstance);
    }
}
