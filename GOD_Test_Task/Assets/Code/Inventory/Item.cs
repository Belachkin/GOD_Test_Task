using System;
using UnityEngine;

namespace Code.Inventory
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public ItemData itemData;
        
        private InventoryManager inventoryManager;

        private void Start()
        {
            inventoryManager = FindObjectOfType<InventoryManager>();
            
            spriteRenderer.sprite = ItemIcons.instance.icons[itemData.Item.ID];
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                inventoryManager.AddItem(itemData.Item, itemData.Quantity);
                Destroy(gameObject);
            }
        }
    }
}