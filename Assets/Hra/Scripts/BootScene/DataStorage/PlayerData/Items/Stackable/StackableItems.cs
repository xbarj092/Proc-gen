using System;
using System.Collections.Generic;

[Serializable]
public class StackableItems
{
    public List<StackableItemSpace> StackableItemSpaces;

    public StackableItems(List<StackableItemSpace> stackableItemSpaces)
    {
        StackableItemSpaces = stackableItemSpaces;
    }
}
