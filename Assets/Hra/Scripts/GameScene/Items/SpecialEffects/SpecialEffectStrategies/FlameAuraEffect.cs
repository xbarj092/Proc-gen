using System;

public class FlameAuraEffect : ISpecialEffect
{
    public float BuffAmount { get; private set; }

    public void ApplyEffect(bool enable, float amount)
    {
        SetFlameAura(enable, amount);
    }

    private void SetFlameAura(bool enable, float damageAmount)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        BuffAmount = damageAmount;
        if (enable)
        {
            specialEffectData.FlameAuraDamage += damageAmount;
            specialEffectData.FlameAuraAmount++;
        }
        else
        {
            specialEffectData.FlameAuraDamage -= damageAmount;
            specialEffectData.FlameAuraAmount--;
        }

        specialEffectData.FlameAuraEnabled = specialEffectData.FlameAuraAmount > 0;
        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }
}
