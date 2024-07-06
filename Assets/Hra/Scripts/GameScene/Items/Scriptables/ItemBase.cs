using UnityEngine;

public enum ItemType
{
    None = 0,
    Weapon = 1,
    Armor = 2,
    Buff = 3,
    Consumable = 4
}

public enum RarityType
{
    None = 0,
    Common = 1,
    Rare = 2,
    Legendary = 3
}

public class ItemBase : ScriptableObject
{
    public string Name;
    public string FriendlyID;
    public string Description;

    public Sprite Icon;

    public RarityType Rarity;
}
