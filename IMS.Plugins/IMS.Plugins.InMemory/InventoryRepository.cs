﻿using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class InventoryRepository : IInventoryRepository
    {
        private List<Inventory> _inventories;

        public InventoryRepository()
        {
            _inventories = new List<Inventory>()
            {
                new Inventory()
                {
                    InventoryId = 1, InventoryName = "Bike Seat", Quantity = 10, Price = 2
                },
                new Inventory()
                {
                     InventoryId = 2, InventoryName = "Bike Body", Quantity = 10, Price = 15
                },
                   new Inventory()
                {
                     InventoryId = 3, InventoryName = "Bike Wheel", Quantity = 20, Price = 8
                },
                new Inventory()
                {
                     InventoryId = 4, InventoryName = "Bike Pedal", Quantity = 20, Price = 1
                },
             };
            }

        public Task AddInventoryAsync(Inventory inventory)
        {
            if (_inventories.Any(x => x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
                {
                  return Task.CompletedTask;
                }
            var maxId = _inventories.Max(x => x.InventoryId);
            inventory.InventoryId = maxId + 1;
            _inventories.Add(inventory);
            return Task.CompletedTask;
        }


        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            if(string.IsNullOrEmpty(name)) return await Task.FromResult(_inventories);

            return _inventories.Where(x => x.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Inventory> GetInventoryByIdAsync(int id)
        {
            return await Task.FromResult( _inventories.First(x => x.InventoryId == id));
        }

        public Task UpdateInventoryAsync(Inventory inventory)
        {
            if(_inventories.Any(x => x.InventoryId != inventory.InventoryId
            && x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase))){
                return Task.CompletedTask;
            }
            var inventoryToUpdate = _inventories.Find(x => x.InventoryId == inventory.InventoryId);
            if (inventoryToUpdate != null )
            {
                inventoryToUpdate.Price = inventory.Price;  
                inventoryToUpdate.Quantity = inventory.Quantity;
            }
            return Task.CompletedTask;
        }
    }
}
