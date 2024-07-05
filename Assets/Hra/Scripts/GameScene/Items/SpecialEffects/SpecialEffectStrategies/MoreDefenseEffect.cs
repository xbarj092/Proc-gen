public class MoreDefenseEffect : ISpecialEffect
{
    public void ApplyEffect(SpecialEffectGranter granter, float amount)
    {
        granter.IncreaseDefense(amount);
    }
}
