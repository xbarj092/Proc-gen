using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemDetail : MonoBehaviour
{
    [SerializeField] protected Image _itemIcon;
    [SerializeField] protected TMP_Text _itemName;
    [SerializeField] protected TMP_Text _description;

    protected ItemInstance _selectedItemInstance;

    public virtual void Init(ItemInstance itemInstance)
    {
        _selectedItemInstance = itemInstance;
        _itemIcon.sprite = itemInstance.Icon;
        _itemName.text = itemInstance.Rarity.ToString() + " " + itemInstance.Name;
        _description.text = itemInstance.Description;
    }
}
