using TMPro;
using UnityEngine;

public class ItemPrice : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemPrice;

    private TMP_Text _itemDiscountedPriceInstantiated;

    public void HandleItemPrice(ItemInstance itemInstance)
    {
        if (itemInstance.DiscountedPrice != 0)
        {
            _itemPrice.text = "<s>" + itemInstance.Price.ToString() + "</s>";
            if (_itemDiscountedPriceInstantiated == null)
            {
                _itemDiscountedPriceInstantiated = Instantiate(_itemPrice, transform);
            }

            _itemDiscountedPriceInstantiated.text = itemInstance.DiscountedPrice.ToString();
        }
        else
        {
            _itemPrice.text = itemInstance.Price.ToString();
            if (_itemDiscountedPriceInstantiated != null)
            {
                Destroy(_itemDiscountedPriceInstantiated.gameObject);
            }
        }
    }
}
