using System;

public class StaminaRegenerationEffect : ISpecialEffect
{
    public float BuffAmount { get; private set; }

    public void ApplyEffect(bool enable, float amount)
    {
        ChangeStaminaRegeneration(enable, amount);
    }

    private void ChangeStaminaRegeneration(bool enable, float percentage)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        PlayerStatistics playerStats = LocalDataStorage.Instance.PlayerData.PlayerStatistics;

        if (enable)
        {
            BuffAmount = playerStats.CurrentStaminaRegeneration * percentage;
            specialEffectData.BonusStaminaRegeneration += BuffAmount;
        }
        else
        {
            specialEffectData.BonusStaminaRegeneration -= BuffAmount;
        }

        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }
}
