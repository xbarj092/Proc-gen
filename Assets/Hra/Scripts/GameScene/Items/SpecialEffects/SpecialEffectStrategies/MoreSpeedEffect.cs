public class MoreSpeedEffect : ISpecialEffect
{
    public void ApplyEffect(SpecialEffectGranter granter, float amount)
    {
        granter.IncreaseSpeed(amount);
    }
}
