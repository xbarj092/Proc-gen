using System;

[Serializable]
public class NonStackableWeaponInstance : NonStackableItemInstance
{
    public int Damage;
    public int Range;
    public float AttacksPerSecond;
    public bool Pierce;

    public NonStackableWeaponInstance(string friendlyID, RarityType rarity, int damage, int range, float attacksPerSecond, bool pierce) :
        base(friendlyID, rarity)
    {
        Damage = damage;
        Range = range;
        AttacksPerSecond = attacksPerSecond;
        Pierce = pierce;
    }
}
