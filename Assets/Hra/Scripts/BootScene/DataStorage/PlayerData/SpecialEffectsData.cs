using System;

[Serializable]
public class SpecialEffectsData
{
    public float BonusStamina;
    public float BonusStaminaRegeneration;
    public float BonusDefense;
    public float BonusDamage;
    public float BonusSpeed;

    public bool FlameAuraEnabled;
    public int FlameAuraAmount;
    public float FlameAuraDamage;

    public bool DamageCrystalEnabled;
    public int DamageCrystalAmount;
    public float CrystalDamage;

    public SpecialEffectsData(float bonusStamina, float bonusStaminaRegeneration, float bonusDefense, float bonusDamage, float bonusSpeed, 
        bool flameAuraEnabled, int flameAuraAmount, float flameAuraDamage, bool damageCrystalEnabled, int damageCrystalAmount, float crystalDamage)
    {
        BonusStamina = bonusStamina;
        BonusStaminaRegeneration = bonusStaminaRegeneration;
        BonusDefense = bonusDefense;
        BonusDamage = bonusDamage;
        BonusSpeed = bonusSpeed;

        FlameAuraEnabled = flameAuraEnabled;
        FlameAuraAmount = flameAuraAmount;
        FlameAuraDamage = flameAuraDamage;

        DamageCrystalEnabled = damageCrystalEnabled;
        DamageCrystalAmount = damageCrystalAmount;
        CrystalDamage = crystalDamage;
    }
}
