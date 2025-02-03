using System.Collections.Generic;

namespace Code.Inventory
{
    public class Inventory
    {
        private List<InventoryItem> items;

        public Inventory()
        {
            items = new List<InventoryItem>();
        }

        public void AddItem(InventoryItem item)
        {
            items.Add(item);
        }

        public List<InventoryItem> GetItemList()
        {
            return items;
        }
    }
}