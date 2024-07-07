using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSelection : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> _slotList = new();

    public event Action<ItemInstance> OnSlotClicked;

    private void OnEnable()
    {
        foreach (ItemSlot slot in _slotList)
        {
            slot.OnSlotClicked += OnSlotClicked;
        }
    }

    private void OnDisable()
    {
        foreach (ItemSlot slot in _slotList)
        {
            slot.OnSlotClicked -= OnSlotClicked;
        }
    }

    public void UpdateSlotList(List<ItemInstance> newItems)
    {
        for (int i = 0; i < newItems.Count; i++)
        {
            _slotList[i].Init(newItems[i]);
        }
    }
}
