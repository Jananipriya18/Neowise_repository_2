using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;

namespace dotnetapp.Services
{
    public class InventoryService
    {
        private static List<InventoryItem> _inventoryItems = new List<InventoryItem>
        {
            new InventoryItem { ItemId = 1, ItemName = "Laptop", Quantity = 50, Price = 999.99m, Category = "Electronics" },
            new InventoryItem { ItemId = 2, ItemName = "Desk Chair", Quantity = 150, Price = 89.99m, Category = "Furniture" },
            new InventoryItem { ItemId = 3, ItemName = "Pen", Quantity = 500, Price = 0.99m, Category = "Stationery" }
        };

        public IEnumerable<InventoryItem> GetAllItems()
        {
            return _inventoryItems;
        }

        public InventoryItem GetItemById(int itemId)
        {
            return _inventoryItems.FirstOrDefault(p => p.ItemId == itemId);
        }

        public void AddItem(InventoryItem newItem)
        {
            // Assuming item IDs are unique and generated elsewhere
            _inventoryItems.Add(newItem);
        }
    }
}
