using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        private InventoryItem inventoryItem;
        
        [SerializeField] private GameObject _slot;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _textQuantity;
        private int quantity;
        public int Quantity => quantity;
        public InventoryItem InventoryItem => inventoryItem;
        
        public void AddQuantity(int _quantity)
        {
            quantity += _quantity;
            
        }

        public void SetItem(InventoryItem _inventoryItem)
        {
            _slot.SetActive(true);
            _icon.sprite = _inventoryItem.Item.Icon;
            
            if (_inventoryItem.Item.Stackable == false)
            {
                _textQuantity.text = "";
            }
            else
            {
                _textQuantity.text = quantity.ToString();
            }
            
        }

        public void ClearSlot()
        {
            _slot.SetActive(false);
        }
    }
}