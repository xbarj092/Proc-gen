using System;
using System.Collections.Generic;

public class ItemGranter
{
    public void GrantItem()
    {
        List<ItemBase> allItems = LocalDataStorage.Instance.CatalogItems.CatalogItemList;
        foreach (ItemBase item in allItems)
        {
            
        }
    }

    private List<ItemBase> GetAllItems()
    {
        throw new NotImplementedException();
    }
}
