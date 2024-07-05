using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreStaminaEffect : ISpecialEffect
{
    public void ApplyEffect(SpecialEffectGranter granter, float amount)
    {
        granter.IncreaseStamina(amount);
    }
}
