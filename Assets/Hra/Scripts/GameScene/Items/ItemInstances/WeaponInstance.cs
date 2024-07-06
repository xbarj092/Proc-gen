public class WeaponInstance : ItemInstance
{
    public int Damage;
    public int Range;
    public float AttacksPerSecond;
    public bool Pierce;

    public WeaponInstance(WeaponItem item) : base(item)
    {
        Damage = item.Damage;
        Range = item.Range;
        AttacksPerSecond = item.AttacksPerSecond;
        Pierce = item.Pierce;
    }
}
