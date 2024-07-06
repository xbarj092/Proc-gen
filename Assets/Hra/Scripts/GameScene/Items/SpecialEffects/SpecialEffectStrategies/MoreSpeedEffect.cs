using System;

public class MoreSpeedEffect : ISpecialEffect
{
    public float BuffAmount { get; private set; }

    public void ApplyEffect(bool enable, float amount)
    {
        ChangeSpeed(enable, amount);
    }

    private void ChangeSpeed(bool enable, float percentage)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        PlayerStatistics playerStats = LocalDataStorage.Instance.PlayerData.PlayerStatistics;

        if (enable)
        {
            BuffAmount = playerStats.CurrentSpeed * percentage;
            specialEffectData.BonusSpeed += BuffAmount;
        }
        else
        {
            specialEffectData.BonusSpeed -= BuffAmount;
        }

        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }
}
