using System;
using System.Collections.Generic;

[Serializable]
public class EquippedItemsData
{
    public string EquippedWeapon;
    public List<string> EquippedArmor;
    public List<string> EquippedBuffs;
    public List<string> EquippedConsumables;

    public EquippedItemsData(string equippedWeapon, List<string> equippedArmor, 
        List<string> equippedBuffs, List<string> equippedConsumables)
    {
        EquippedWeapon = equippedWeapon;
        EquippedArmor = equippedArmor;
        EquippedBuffs = equippedBuffs;
        EquippedConsumables = equippedConsumables;
    }
}
