using System;

[Serializable]
public class NonStackableItemInstance
{
    public string FriendlyID;
    public RarityType Rarity;
    public int InstanceID;

    public NonStackableItemInstance(string friendlyID, RarityType rarity, int instanceID)
    {
        FriendlyID = friendlyID;
        Rarity = rarity;
        InstanceID = instanceID;
    }
}
