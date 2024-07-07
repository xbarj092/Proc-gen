using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemInstance
{
    public string Name;
    public string FriendlyID;
    public string Description;

    public Sprite Icon;

    public RarityType Rarity;

    public float Price;

    public int InstanceID;

    public ItemInstance(ItemBase item)
    {
        Name = item.Name;
        FriendlyID = item.FriendlyID;
        Description = item.Description;

        Icon = item.Icon;

        Rarity = item.Rarity;

        Price = item.Price;
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
