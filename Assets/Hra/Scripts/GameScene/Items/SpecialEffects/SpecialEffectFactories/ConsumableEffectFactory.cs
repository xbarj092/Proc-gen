public class ConsumableEffectFactory
{
    public ISpecialEffect CreateEffect(SpecialConsumableEffect effect)
    {
        return effect switch
        {
            SpecialConsumableEffect.MoreDefense => new MoreDefenseEffect(),
            SpecialConsumableEffect.MoreSpeed => new MoreSpeedEffect(),
            SpecialConsumableEffect.StaminaRegeneration => new StaminaRegenerationEffect(),
            _ => null,
        };
    }
}
