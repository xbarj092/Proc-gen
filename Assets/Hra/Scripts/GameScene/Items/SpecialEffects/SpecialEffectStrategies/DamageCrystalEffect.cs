using System;

public class DamageCrystalEffect : ISpecialEffect
{
    public float BuffAmount { get; private set; }

    public void ApplyEffect(bool enable, float amount)
    {
        SetDamageCrystal(enable, amount);
    }

    private void SetDamageCrystal(bool enable, float damageAmount)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        BuffAmount = damageAmount;
        if (enable)
        {
            specialEffectData.CrystalDamage += damageAmount;
            specialEffectData.DamageCrystalAmount++;
        }
        else
        {
            specialEffectData.CrystalDamage -= damageAmount;
            specialEffectData.DamageCrystalAmount--;
        }

        specialEffectData.DamageCrystalEnabled = specialEffectData.DamageCrystalAmount > 0;
        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }
}
