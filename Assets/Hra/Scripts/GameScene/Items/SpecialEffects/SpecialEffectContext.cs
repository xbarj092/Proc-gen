using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectContext
{
    private ISpecialEffect _specialEffect;

    public void SetEffect(ISpecialEffect effect)
    {
        _specialEffect = effect;
    }

    public void ApplyEffect(SpecialEffectGranter granter, float amount)
    {
        _specialEffect.ApplyEffect(granter, amount);
    }
}
