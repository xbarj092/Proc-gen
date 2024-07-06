public class ItemInstanceFactory
{
    public ItemInstance CreateItemInstance(ItemBase itemBase)
    {
        return itemBase switch
        {
            WeaponItem weaponItem => new WeaponInstance(weaponItem),
            ArmorItem armorItem => new ArmorInstance(armorItem),
            BuffItem buffItem => new BuffInstance(buffItem),
            ConsumableItem consumableItem => new ConsumableInstance(consumableItem),
            _ => null,
        };
    }
}
