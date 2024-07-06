using System;

public class MoreDamageDealt : ISpecialEffect
{
    public float BuffAmount { get; private set; }

    public void ApplyEffect(bool enable, float amount)
    {
        ChangeDamageDealt(enable, amount);
    }

    private void ChangeDamageDealt(bool enable, float percentage)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        PlayerStatistics playerStats = LocalDataStorage.Instance.PlayerData.PlayerStatistics;

        if (enable)
        {
            BuffAmount = playerStats.CurrentDamage * percentage;
            specialEffectData.BonusDamage += BuffAmount;
        }
        else
        {
            specialEffectData.BonusDamage -= BuffAmount;
        }

        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }
}
