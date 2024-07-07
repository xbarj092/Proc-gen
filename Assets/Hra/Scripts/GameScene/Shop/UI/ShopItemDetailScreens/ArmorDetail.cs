using UnityEngine;
using TMPro;

public class ArmorDetail : ItemDetail
{
    [SerializeField] private ItemEffects _itemEffects;
    [SerializeField] private TMP_Text _defense;

    public override void Init(ItemInstance itemInstance)
    {
        base.Init(itemInstance);
        ArmorInstance armorInstance = itemInstance as ArmorInstance;
        InitArmor(armorInstance);
    }

    private void InitArmor(ArmorInstance armorInstance)
    {
        // _specialEffects.text = armorInstance.SpecialArmorEffects.ToString();
        _itemEffects.Init(armorInstance);
        _defense.text = armorInstance.Defense.ToString();
    }
}
