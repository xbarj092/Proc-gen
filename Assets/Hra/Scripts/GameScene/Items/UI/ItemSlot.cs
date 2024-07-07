using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemSlot : MonoBehaviour
{
    [SerializeField] private ItemPrice _itemPrice;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TMP_Text _itemName;

    private ItemInstance _itemInstance;

    public event Action<ItemInstance> OnSlotClicked;

    public void Init(ItemInstance itemInstance)
    {
        _itemInstance = itemInstance;

        if (itemInstance.Icon != null)
        {
            _itemIcon.sprite = itemInstance.Icon;
        }

        _itemName.text = itemInstance.Name;
        _itemPrice.HandleItemPrice(itemInstance);
    }

    // bound from inspector
    public void SlotClick()
    {
        OnSlotClicked?.Invoke(_itemInstance);
    }
}
 