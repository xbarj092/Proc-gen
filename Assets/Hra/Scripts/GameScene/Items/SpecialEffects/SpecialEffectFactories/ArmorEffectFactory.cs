public class ArmorEffectFactory
{
    public ISpecialEffect CreateEffect(SpecialArmorEffect effect)
    {
        return effect switch
        {
            SpecialArmorEffect.MoreDefense => new MoreDefenseEffect(),
            SpecialArmorEffect.FlameAura => new FlameAuraEffect(),
            SpecialArmorEffect.MoreSpeed => new MoreSpeedEffect(),
            _ => null,
        };
    }
}
