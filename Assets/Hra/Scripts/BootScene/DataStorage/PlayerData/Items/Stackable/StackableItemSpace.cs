using System;

[Serializable]
public class StackableItemSpace
{
    public string FriendlyID;
    public int Amount;

    public StackableItemSpace(string friendlyID, int amount)
    {
        FriendlyID = friendlyID;
        Amount = amount;
    }
}
