using System;
using UnityEngine;

namespace Code.Inventory
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public ItemSO _ItemSO;
        public int Quantity;
        
        private InventoryManager inventoryManager;

        private void Start()
        {
            inventoryManager = FindObjectOfType<InventoryManager>();
            spriteRenderer.sprite = _ItemSO.Icon;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                inventoryManager.AddItem(_ItemSO, Quantity);
                Destroy(gameObject);
            }
        }
    }
}