using System;

[Serializable]
public class PlayerStatistics
{
    public float CurrentHealth;
    public float MaxHealth;

    public float CurrentStamina;
    public float MaxStamina;
    public float CurrentStaminaRegeneration;

    public float CurrentDefense;
    public float CurrentSpeed;
    public float CurrentDamage;

    public PlayerStatistics(float currentHealth, float maxHealth, float currentStamina, float currentStaminaRegeneration, 
        float maxStamina, float currentDefense, float currentSpeed, float currentDamage)
    {
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;

        CurrentStamina = currentStamina;
        CurrentStaminaRegeneration = currentStaminaRegeneration;
        MaxStamina = maxStamina;

        CurrentDefense = currentDefense;
        CurrentSpeed = currentSpeed;
        CurrentDamage = currentDamage;
    }
}
