using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Inventory
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject itemObject;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _quantityText;
        
        [SerializeField] private GameObject _selectedObject;
        
        [HideInInspector] public ItemSO _ItemSO;
        [HideInInspector] public int Quantity;

        [HideInInspector] public bool isFull = false;
        [HideInInspector] public bool isSelected = false;
        
        private InventoryManager inventoryManager;

        private void Start()
        {
            inventoryManager = FindObjectOfType<InventoryManager>();
        }

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
                itemObject.SetActive(true);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                inventoryManager.DeselectAll();
                isSelected = true;
                _selectedObject.SetActive(true);
                
                ViewSlot();
            }
        }

        public void Deselect()
        {
            isSelected = false;
            _selectedObject.SetActive(false);
        }

        public void ViewSlot()
        {
            if (isFull == false)
            {
                inventoryManager.ViewSlot();
            }
            else
            {
                inventoryManager.ViewSlot(_ItemSO, Quantity);
            }
            
        }

        public void Clear()
        {
            Quantity = 0;
            _ItemSO = null;
            _icon.sprite = null;
            _quantityText.text = "";
            isFull = false;
            inventoryManager.DeselectAll();
            itemObject.SetActive(false);
        }
    }
}