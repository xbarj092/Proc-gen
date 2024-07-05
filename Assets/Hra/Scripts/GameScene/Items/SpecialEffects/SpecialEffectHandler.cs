using System.Collections.Generic;

public class SpecialEffectHandler
{
    private SpecialEffectGranter _specialEffectGranter { get; }
    private BuffEffectFactory _buffEffectFactory = new();
    private ArmorEffectFactory _armorEffectFactory = new();
    private ConsumableEffectFactory _consumableEffectFactory = new();

    public void ApplyArmorEffects(ArmorItem armorItem)
    {
        SpecialEffectContext context = new();

        foreach (KeyValuePair<SpecialArmorEffect, float> effect in armorItem.SpecialArmorEffects)
        {
            ISpecialEffect armorEffect = _armorEffectFactory.CreateEffect(effect.Key);
            if (armorEffect != null)
            {
                context.SetEffect(armorEffect);
                context.ApplyEffect(_specialEffectGranter, effect.Value);
            }
        }
    }

    public void ApplyBuffEffects(BuffItem buffItem)
    {
        SpecialEffectContext context = new();

        foreach (KeyValuePair<SpecialBuffEffect, float> effect in buffItem.SpecialBuffEffects)
        {
            ISpecialEffect buffEffect = _buffEffectFactory.CreateEffect(effect.Key);
            if (buffEffect != null)
            {
                context.SetEffect(buffEffect);
                context.ApplyEffect(_specialEffectGranter, effect.Value);
            }
        }
    }

    public void ApplyConsumableEffects(ConsumableItem consumableItem)
    {
        SpecialEffectContext context = new();

        foreach (KeyValuePair<SpecialConsumableEffect, float> effect in consumableItem.SpecialConsumableEffects)
        {
            ISpecialEffect consumableEffect = _consumableEffectFactory.CreateEffect(effect.Key);
            if (consumableEffect != null)
            {
                context.SetEffect(consumableEffect);
                context.ApplyEffect(_specialEffectGranter, effect.Value);
            }
        }
    }
}
