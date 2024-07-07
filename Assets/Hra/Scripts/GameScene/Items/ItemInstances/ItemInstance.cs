using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ItemInstance
{
    public string Name;
    public string FriendlyID;
    public string Description;

    public Sprite Icon;

    public RarityType Rarity;

    public int Price;
    public int DiscountedPrice;

    public int InstanceID;

    public ItemInstance(ItemBase item)
    {
        Name = item.Name;
        FriendlyID = item.FriendlyID;
        Description = item.Description;

        Icon = item.Icon;

        Rarity = item.Rarity;

        Price = Mathf.CeilToInt(item.Price);
    }

    protected int GenerateInstanceID<T>(List<T> instances) where T : NonStackableItemInstance
    {
        System.Random random = new(); 
        int instanceID = default;

        do
        {
            instanceID = random.Next(0, 1000000);
        }
        while (instances.Any(instance => instance.InstanceID == instanceID));
        return instanceID;
    }
}
