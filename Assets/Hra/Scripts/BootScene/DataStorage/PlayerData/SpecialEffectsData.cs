public class SpecialEffectsData
{
    public float BonusStamina;
    public float BonusStaminaRegeneration;
    public float BonusDefense;
    public float BonusDamage;
    public float BonusSpeed;

    public bool FlameAuraEnabled;
    public float FlameAuraDamage;
    public bool DamageCrystalEnabled;
    public float CrystalDamage;

    public SpecialEffectsData(float bonusStamina, float bonusStaminaRegeneration, float bonusDefense, float bonusDamage, 
        float bonusSpeed, bool flameAuraEnabled, float flameAuraDamage, bool damageCrystalEnabled, float crystalDamage)
    {
        BonusStamina = bonusStamina;
        BonusStaminaRegeneration = bonusStaminaRegeneration;
        BonusDefense = bonusDefense;
        BonusDamage = bonusDamage;
        BonusSpeed = bonusSpeed;

        FlameAuraEnabled = flameAuraEnabled;
        FlameAuraDamage = flameAuraDamage;
        DamageCrystalEnabled = damageCrystalEnabled;
        CrystalDamage = crystalDamage;
    }
}
