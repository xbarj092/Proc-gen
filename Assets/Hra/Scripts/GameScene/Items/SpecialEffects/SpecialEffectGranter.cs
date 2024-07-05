public class SpecialEffectGranter
{
    public void IncreaseDefense(float percentage)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        PlayerStatistics playerStats = LocalDataStorage.Instance.PlayerData.PlayerStatistics;

        specialEffectData.BonusDefense += playerStats.CurrentDefense * percentage;
        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }

    public void IncreaseSpeed(float percentage)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        PlayerStatistics playerStats = LocalDataStorage.Instance.PlayerData.PlayerStatistics;

        specialEffectData.BonusSpeed += playerStats.CurrentSpeed * percentage;
        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }

    public void IncreaseDamageDealt(float percentage)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        PlayerStatistics playerStats = LocalDataStorage.Instance.PlayerData.PlayerStatistics;

        specialEffectData.BonusDamage += playerStats.CurrentDamage * percentage;
        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }

    public void IncreaseStamina(float percentage)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        PlayerStatistics playerStats = LocalDataStorage.Instance.PlayerData.PlayerStatistics;

        specialEffectData.BonusStamina += playerStats.MaxStamina * percentage;
        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }

    public void IncreaseStaminaRegeneration(float percentage)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        PlayerStatistics playerStats = LocalDataStorage.Instance.PlayerData.PlayerStatistics;

        specialEffectData.BonusStaminaRegeneration += playerStats.CurrentStaminaRegeneration * percentage;
        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }

    public void SetFlameAura(bool enable, float damageAmount)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        specialEffectData.FlameAuraEnabled = enable;
        if (enable)
        {
            specialEffectData.FlameAuraDamage += damageAmount;
        }
        else
        {
            specialEffectData.FlameAuraDamage -= damageAmount;
        }

        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }

    public void SetDamageCrystal(bool enable, float damageAmount)
    {
        SpecialEffectsData specialEffectData = LocalDataStorage.Instance.PlayerData.SpecialEffectsData;
        specialEffectData.DamageCrystalEnabled = enable;
        if (enable)
        {
            specialEffectData.CrystalDamage += damageAmount;
        }
        else
        {
            specialEffectData.CrystalDamage -= damageAmount;
        }

        LocalDataStorage.Instance.PlayerData.SpecialEffectsData = specialEffectData;
    }
}
