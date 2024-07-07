using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : GameScreen
{
    [SerializeField] private ShopItemDetail _shopItemDetail;
    [SerializeField] private ShopItemSelection _shopItemSelection;

    private ShopHelper _shopHelper = new();

    private List<ItemInstance> _itemsToSell;
    private ItemInstance _selectedItemInstance;

    private void OnEnable()
    {
        _shopItemSelection.OnSlotClicked += ShowItemDetail;
        GetNewItems();
    }

    private void OnDisable()
    {
        _shopItemSelection.OnSlotClicked -= ShowItemDetail;
    }

    public void SellItems()
    {

    }

    public void SellSelectedItems()
    {
        _shopHelper.SellItems(_itemsToSell);
    }

    public void GetNewItems()
    {
        List<ItemInstance> newItems = _shopHelper.GetNewItems();
        _shopItemSelection.UpdateSlotList(newItems);
    }

    public void ShowItemDetail(ItemInstance itemInstance)
    {
        _selectedItemInstance = itemInstance;
        _shopItemDetail.gameObject.SetActive(true);
        _shopItemSelection.gameObject.SetActive(false);
        _shopItemDetail.Init(_selectedItemInstance);
        _shopItemDetail.OnItemPurchased += BuyItem;
        _shopItemDetail.OnDetailClosed += HideItemDetail;
    }

    public void HideItemDetail()
    {
        _shopItemDetail.gameObject.SetActive(false);
        _shopItemSelection.gameObject.SetActive(true);
        _shopItemDetail.OnItemPurchased -= BuyItem;
        _shopItemDetail.OnDetailClosed -= HideItemDetail;
    }

    private void BuyItem(ItemInstance itemInstance)
    {
        HideItemDetail();
        _shopHelper.PurchaseItem(itemInstance);
    }
}
