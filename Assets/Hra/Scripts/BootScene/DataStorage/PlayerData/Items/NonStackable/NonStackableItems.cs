using System;
using System.Collections.Generic;

[Serializable]
public class NonStackableItems
{
    public List<NonStackableWeaponInstance> NonStackableWeaponInstances;
    public List<NonStackableArmorInstance> NonStackableArmorInstances;
    public List<NonStackableBuffInstance> NonStackableBuffInstances;

    public NonStackableItems(List<NonStackableWeaponInstance> nonStackableWeaponInstances, 
        List<NonStackableArmorInstance> nonStackableArmorInstances, List<NonStackableBuffInstance> nonStackableBuffInstances)
    {
        NonStackableWeaponInstances = nonStackableWeaponInstances;
        NonStackableArmorInstances = nonStackableArmorInstances;
        NonStackableBuffInstances = nonStackableBuffInstances;
    }
}
