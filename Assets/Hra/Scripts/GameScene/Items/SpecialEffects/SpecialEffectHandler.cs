using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectHandler
{
    private static DummyMonoBehaviour _coroutineStarterMonoBehaviour;
    private static DummyMonoBehaviour CoroutineStarterMonoBehaviour
    {
        get
        {
            if (_coroutineStarterMonoBehaviour == null)
            {
                GameObject loaderGameObject = new("Coroutine Starter Game Object");
                _coroutineStarterMonoBehaviour = loaderGameObject.AddComponent<DummyMonoBehaviour>();
            }

            return _coroutineStarterMonoBehaviour;
        }
    }

    private BuffEffectFactory _buffEffectFactory = new();
    private ArmorEffectFactory _armorEffectFactory = new();
    private ConsumableEffectFactory _consumableEffectFactory = new();

    private List<ISpecialEffect> _specialEffectsActive = new();

    public void ApplyArmorEffects(ArmorItem armorItem, bool enable)
    {
        ApplyEffects(armorItem.SpecialArmorEffects, _armorEffectFactory, enable);
    }

    public void ApplyBuffEffects(BuffItem buffItem, bool enable)
    {
        ApplyEffects(buffItem.SpecialBuffEffects, _buffEffectFactory, enable);
    }

    public void ApplyConsumableEffects(ConsumableItem consumableItem, bool enable)
    {
        ApplyEffects(consumableItem.SpecialConsumableEffects, _consumableEffectFactory, enable, consumableItem.Duration);
    }

    private void ApplyEffects<T>(Dictionary<T, float> effects, IEffectFactory<T> factory, bool enable, float duration = default)
    {
        foreach (KeyValuePair<T, float> effect in effects)
        {
            ISpecialEffect specialEffect = factory.CreateEffect(effect.Key);
            if (specialEffect != null)
            {
                specialEffect.ApplyEffect(enable, effect.Value);
                _specialEffectsActive.Add(specialEffect);
                if (enable && duration != default)
                {
                    CoroutineStarterMonoBehaviour.StartCoroutine(StartCountdown(specialEffect, duration));
                }
            }
        }
    }

    private IEnumerator StartCountdown(ISpecialEffect specialEffect, float duration)
    {
        float durationLeft = duration;
        while (durationLeft > 0)
        {
            yield return new WaitForSeconds(1);
            durationLeft--;
        }

        specialEffect.ApplyEffect(false, 0);
        _specialEffectsActive.Remove(specialEffect);
    }
}
