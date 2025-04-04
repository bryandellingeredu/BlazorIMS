using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class ProductTransactionRepository : IProductTransactionRepository
    {
        private List<ProductTransaction> _productTransactions = new List<ProductTransaction>();
        private readonly IProductRepository _productRepository;
        private readonly IInventoryTransactionRepository _inventoryTransactionRepository;
        private readonly IInventoryRepository _inventoryRepository;
        public ProductTransactionRepository(
       IProductRepository productRepository,
       IInventoryTransactionRepository inventoryTransactionRepository,
       IInventoryRepository inventoryRepository
   )
        {
              _productRepository = productRepository;
              _inventoryTransactionRepository = inventoryTransactionRepository;  
            _inventoryRepository = inventoryRepository; 
        }
        public async Task ProduceAsync(string productionNumber, Product product, int quantity,  string doneBy)
        {

            var prod = await _productRepository.GetProductByIdAsync(product.ProductId);
            if (prod != null)
            {
              foreach (var pi in prod.ProductInventories)
                {
                   if(pi.Inventory != null)
                    {
                        // add inventory transaction
                        await _inventoryTransactionRepository.ProduceAsync(
                       productionNumber,
                       pi.Inventory,
                       pi.InventoryQuantity * quantity,
                       doneBy,
                       -1);

                        // decrease the inventories
                        var inv = await _inventoryRepository.GetInventoryByIdAsync(pi.InventoryId);
                        inv.Quantity -= pi.InventoryQuantity * quantity;
                        await _inventoryRepository.UpdateInventoryAsync(inv);   
                    }
                
                }
            }

            // add product transaction
            _productTransactions.Add(new ProductTransaction
            {
                ProductionNumber = productionNumber,    
                ProductId = product.ProductId,  
                QuantityBefore = product.Quantity,
                ActivityType = ProductTransactionType.ProduceProduct,
                QuantityAfter = product.Quantity + quantity,
                TransactionDate = DateTime.UtcNow,
                DoneBy = doneBy,
            });

        }
    }
}
