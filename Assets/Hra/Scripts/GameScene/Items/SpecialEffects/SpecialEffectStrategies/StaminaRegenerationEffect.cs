using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaRegenerationEffect : ISpecialEffect
{
    public void ApplyEffect(SpecialEffectGranter granter, float amount)
    {
        granter.IncreaseStaminaRegeneration(amount);
    }
}
