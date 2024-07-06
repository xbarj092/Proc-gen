using UnityEngine;

public class ItemBase : ScriptableObject
{
    public string Name;
    public string FriendlyID;
    public string Description;

    public Sprite Icon;

    public RarityType Rarity;

    public float Price;
}
