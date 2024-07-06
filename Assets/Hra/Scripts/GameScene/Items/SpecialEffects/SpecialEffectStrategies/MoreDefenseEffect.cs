using System;

public class MoreDefenseEffect : ISpecialEffect
{
    public float BuffAmount { get; private set; }

    public void ApplyEffect(bool enable, float amount)
    {
        ChangeDefense(enable, amount);
    }

    private void ChangeDefense(bool enable, float percentage)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        PlayerStatistics playerStats = LocalDataStorage.Instance.PlayerData.PlayerStatistics;

        if (enable)
        {
            BuffAmount = playerStats.CurrentDefense * percentage;
            specialEffectData.BonusDefense += BuffAmount;
        }
        else
        {
            specialEffectData.BonusDefense -= BuffAmount;
        }

        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }
}
