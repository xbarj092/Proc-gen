using System;

[Serializable]
public class NonStackableItemInstance
{
    public string FriendlyID;
    public RarityType Rarity;

    public NonStackableItemInstance(string friendlyID, RarityType rarity)
    {
        FriendlyID = friendlyID;
        Rarity = rarity;
    }
}
