using TMPro;
using UnityEngine;

public class ConsumableDetail : ItemDetail
{
    [SerializeField] private ItemEffects _itemEffects;
    [SerializeField] private TMP_Text _duration;

    public override void Init(ItemInstance itemInstance)
    {
        base.Init(itemInstance);
        ConsumableInstance consumableInstance = itemInstance as ConsumableInstance;
        InitConsumable(consumableInstance);
    }

    private void InitConsumable(ConsumableInstance consumableInstance)
    {
        _itemEffects.Init(consumableInstance);
        _duration.text = consumableInstance.Duration.ToString();
    }
}
