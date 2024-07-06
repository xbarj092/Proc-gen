using System;

public class MoreStaminaEffect : ISpecialEffect
{
    public float BuffAmount { get; private set; }

    public void ApplyEffect(bool enable, float amount)
    {
        ChangeStamina(enable, amount);
    }

    private void ChangeStamina(bool enable, float percentage)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        PlayerStatistics playerStats = LocalDataStorage.Instance.PlayerData.PlayerStatistics;

        if (enable)
        {
            BuffAmount = playerStats.MaxStamina * percentage;
            specialEffectData.BonusStamina += BuffAmount;
        }
        else
        {
            specialEffectData.BonusStamina -= BuffAmount;
        }

        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }
}
