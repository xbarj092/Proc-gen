using UnityEngine;
using TMPro;

public class WeaponDetail : ItemDetail
{
    [SerializeField] private TMP_Text _damage;
    [SerializeField] private TMP_Text _range;
    [SerializeField] private TMP_Text _attacksPerSecond;
    [SerializeField] private TMP_Text _pierce;

    public override void Init(ItemInstance itemInstance)
    {
        base.Init(itemInstance);
        WeaponInstance weaponInstance = itemInstance as WeaponInstance;
        InitWeapon(weaponInstance);
    }

    private void InitWeapon(WeaponInstance weaponInstance)
    {
        _damage.text = weaponInstance.Damage.ToString();
        _range.text = weaponInstance.Range.ToString();
        _attacksPerSecond.text = weaponInstance.AttacksPerSecond.ToString();
        _pierce.text = weaponInstance.Pierce.ToString();
    }
}
