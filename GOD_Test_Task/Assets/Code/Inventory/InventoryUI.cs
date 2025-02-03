using System.Collections.Generic;
using UnityEngine;

namespace Code.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private List<InventorySlot> inventorySlots = new List<InventorySlot>();
        private Inventory inventory;
        public void SetInventory(Inventory _inventory)
        {
            inventory = _inventory;
        }

        public void RefreshInventoryItems()
        {
            
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                inventorySlots[i].ClearSlot();
            }

            List<InventoryItem> items = inventory.GetItemList();

            int slotIndex = 0;
            foreach (var item in items)
            {
                if(slotIndex >= inventorySlots.Count) break;

                if (item.Item.Stackable == true)
                {

                    int temp = item.Quantity;

                    while (temp > 0 && slotIndex < inventorySlots.Count)
                    {
                        if (temp > item.Item.StackSize)
                        {
                            inventorySlots[slotIndex].AddQuantity(item.Item.StackSize);
                            inventorySlots[slotIndex].SetItem(item);
                            temp -= item.Item.StackSize;
                        }
                        else
                        {
                            inventorySlots[slotIndex].AddQuantity(temp);
                            inventorySlots[slotIndex].SetItem(item);
                            temp = 0;
                            break;
                        }

                        slotIndex++;
                    }
                }
                else
                {
                    int temp = item.Quantity;
                    for (int i = item.Quantity; i > 0; i--)
                    {
                        inventorySlots[slotIndex].SetItem(item);
                        temp--;
                        slotIndex++;
                    }
                }
                
                slotIndex++;
            }

            
        }
    }
}