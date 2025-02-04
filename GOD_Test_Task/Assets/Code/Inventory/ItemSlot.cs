using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Inventory
{
    public class ItemSlot : MonoBehaviour

    {
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _quantityText;
    
    [HideInInspector] public ItemSO _ItemSO;
    [HideInInspector] public int Quantity;

    [HideInInspector] public bool isFull = false;

    public void AddItem(ItemSO item, int quantity)
    {
        if (_ItemSO != null && _ItemSO.ID == item.ID)
        {
            Quantity += quantity; 
        }
        else
        {
            _ItemSO = item;
            Quantity = quantity;
        }
        
        if (_ItemSO != null)
        {
            _icon.sprite = _ItemSO.Icon;
            _quantityText.text = Quantity.ToString();
            isFull = true;
        }
    }
    }
}