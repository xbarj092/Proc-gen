using EasyButtons;
using UnityEngine;

public class MethodTester : MonoBehaviour
{
    private SpecialEffectHandler _specialEffectHandler = new();
    private InventoryHelper _inventoryHelper = new();

    private ItemGenerator _itemGenerator = new();

    [SerializeField] private ItemBase _item;

    [Button]
    public void GrantBuffSpecialEffects()
    {
        switch (_item)
        {
            case BuffItem buffItem:
                _specialEffectHandler.ApplyBuffEffects(buffItem, true);
                break;
            case ArmorItem armorItem:
                _specialEffectHandler.ApplyArmorEffects(armorItem, true);
                break;
            case ConsumableItem consumableItem:
                _specialEffectHandler.ApplyConsumableEffects(consumableItem, true);
                break;
        }
    }

    [Button]
    public void GenerateRandomItem()
    {
        _inventoryHelper.AddItemsToInventory(_itemGenerator.GrantItems(5));
    }
}
