using TMPro;
using UnityEngine;

public class BuffDetail : ItemDetail
{
    [SerializeField] private ItemEffects _itemEffects;

    public override void Init(ItemInstance itemInstance)
    {
        base.Init(itemInstance);
        BuffInstance buffInstance = itemInstance as BuffInstance;
        InitBuff(buffInstance);
    }

    private void InitBuff(BuffInstance buffInstance)
    {
        _itemEffects.Init(buffInstance);
    }
}
