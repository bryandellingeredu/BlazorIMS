﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        public List<InventoryTransaction> _inventoryTransactions = new List<InventoryTransaction>();

        public Task ProduceAsync(string productionNumber, Inventory inventory, int quantityToConsume, string doneBy, double price)
        {
            _inventoryTransactions.Add(new InventoryTransaction
            {
                ProductionNumber = productionNumber,
                InventoryId = inventory.InventoryId,
                QuantityBefore = inventory.Quantity,
                ActivityType = InventoryTransactionType.ProduceProduct,
                QuantityAfter = inventory.Quantity - quantityToConsume,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = price
            });

            return Task.CompletedTask;
        }

        public Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, double price)
        {
           _inventoryTransactions.Add(new InventoryTransaction
                {
                  PONumber = poNumber,
                  InventoryId = inventory.InventoryId,
                  QuantityBefore = inventory.Quantity,  
                  ActivityType = InventoryTransactionType.PurchaseInventory,
                  QuantityAfter = inventory.Quantity + quantity, 
                  TransactionDate = DateTime.Now,
                  DoneBy = doneBy,
                  UnitPrice = price
                });

            return Task.CompletedTask;
        }
    }
}
