public class FlameAuraEffect : ISpecialEffect
{
    public void ApplyEffect(SpecialEffectGranter granter, float amount)
    {
        granter.SetFlameAura(true, amount);
    }
}
