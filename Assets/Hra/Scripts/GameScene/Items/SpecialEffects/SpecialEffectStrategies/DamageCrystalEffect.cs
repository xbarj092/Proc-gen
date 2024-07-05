public class DamageCrystalEffect : ISpecialEffect
{
    public void ApplyEffect(SpecialEffectGranter granter, float amount)
    {
        granter.SetDamageCrystal(true, amount);
    }
}
