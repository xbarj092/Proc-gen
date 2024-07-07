using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{
    [SerializeField] private ItemEffect _itemEffectPrefab;

    [SerializeField] private SerializedDictionary<SpecialArmorEffect, Sprite> _armorEffectIcons = new();
    [SerializeField] private SerializedDictionary<SpecialBuffEffect, Sprite> _buffEffectIcons = new();
    [SerializeField] private SerializedDictionary<SpecialConsumableEffect, Sprite> _consumableEffectIcons = new();

    public void Init(ItemInstance itemInstance)
    {
        switch (itemInstance)
        {
            case WeaponInstance weaponInstance:
                break;
            case ArmorInstance armorInstance:
                InitArmor(armorInstance);
                break;
            case BuffInstance buffInstance:
                InitBuff(buffInstance);
                break;
            case ConsumableInstance consumableInstance:
                InitConsumable(consumableInstance);
                break;
        }
    }

    private void InitArmor(ArmorInstance armorInstance)
    {
        InitSpecialEffects(armorInstance.SpecialArmorEffects);
    }

    private void InitBuff(BuffInstance buffInstance)
    {
        InitSpecialEffects(buffInstance.SpecialBuffEffects);
    }

    private void InitConsumable(ConsumableInstance consumableInstance)
    {
        InitSpecialEffects(consumableInstance.SpecialConsumableEffects);
    }

    private void InitSpecialEffects<T>(Dictionary<T, float> specialEffects) where T : Enum
    {
        foreach (KeyValuePair<T, float> keyValuePair in specialEffects)
        {
            Sprite effectSprite = GetEffectSprite(keyValuePair.Key);
            string text = keyValuePair.Value.ToString();
            ItemEffect newEffect = Instantiate(_itemEffectPrefab, transform);
            newEffect.Init(effectSprite, text);
        }
    }

    private Sprite GetEffectSprite<T>(T key) where T : Enum
    {
        return key switch
        {
            SpecialArmorEffect armorEffect => _armorEffectIcons[armorEffect],
            SpecialBuffEffect buffEffect => _buffEffectIcons[buffEffect],
            SpecialConsumableEffect consumableEffect => _consumableEffectIcons[consumableEffect],
            _ => throw new ArgumentException($"Unsupported effect type: {typeof(T)}"),
        };
    }
}
