public interface IEffectFactory<T>
{
    ISpecialEffect CreateEffect(T effect);
}
