public class BuffEffectFactory
{
    public ISpecialEffect CreateEffect(SpecialBuffEffect effect)
    {
        return effect switch
        {
            SpecialBuffEffect.FlameAura => new FlameAuraEffect(),
            SpecialBuffEffect.MoreDamageDealt => new MoreDamageDealt(),
            SpecialBuffEffect.DamageCrystal => new DamageCrystalEffect(),
            SpecialBuffEffect.MoreStamina => new MoreStaminaEffect(),
            _ => null,
        };
    }
}
