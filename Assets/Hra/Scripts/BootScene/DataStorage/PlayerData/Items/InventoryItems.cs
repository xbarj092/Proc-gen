using System;

[Serializable]
public class InventoryItems
{
    public NonStackableItems NonStackableItems;
    public StackableItems StackableItems;

    public InventoryItems(NonStackableItems nonStackableItems, StackableItems stackableItems)
    {
        NonStackableItems = nonStackableItems;
        StackableItems = stackableItems;
    }
}
