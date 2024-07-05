public class MoreDamageDealt : ISpecialEffect
{
    public void ApplyEffect(SpecialEffectGranter granter, float amount)
    {
        granter.IncreaseDamageDealt(amount);
    }
}
