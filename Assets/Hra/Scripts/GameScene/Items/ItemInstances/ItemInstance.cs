using UnityEngine;

public class ItemInstance
{
    public string Name;
    public string FriendlyID;
    public string Description;

    public Sprite Icon;

    public RarityType Rarity;

    public float Price;

    public ItemInstance(ItemBase item)
    {
        Name = item.Name;
        FriendlyID = item.FriendlyID;
        Description = item.Description;

        Icon = item.Icon;

        Rarity = item.Rarity;

        Price = item.Price;
    }
}
