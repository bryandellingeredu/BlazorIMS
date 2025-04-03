
using IMS.CoreBusiness;
using IMS.CoreBusiness.Validations;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _products;
        public ProductRepository()
        {
            _products = new List<Product>()
            {
                new Product { ProductId = 1, ProductName = "Bike", Quantity = 10, Price = 150 },
                new Product { ProductId = 2, ProductName = "Car", Quantity = 10, Price = 25000},
            };
        }

        public Task AddProductAsync(Product product)
        {
            if (_products.Any(x => x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }
            var maxId = _products.Max(x => x.ProductId);
            product.ProductId = maxId + 1;
            _products.Add(product);
            return Task.CompletedTask;
        }

        public  Task DeleteProductAsync(int id)
        {
              var productToDelete = _products.Find(x => x.ProductId == id);
            if (productToDelete != null) _products.Remove(productToDelete);
            return Task.CompletedTask;
        }



        public async Task<Product> GetProductByIdAsync(int id)
        {
            var prod = _products.First(x => x.ProductId == id);
            List<ProductInventory> productInventories = new List<ProductInventory>();
            if(prod.ProductInventories != null)
            {
                foreach (var prodinv in prod.ProductInventories)
                {
                    Inventory inventory = new Inventory();
                    if (prodinv.Inventory != null)
                    {
                        inventory.InventoryId = prodinv.Inventory.InventoryId;
                        inventory.InventoryName = prodinv.Inventory.InventoryName;
                        inventory.Price = prodinv.Inventory.Price;
                        inventory.Quantity = prodinv.Inventory.Quantity;    
                    }
                    productInventories.Add(new ProductInventory
                    {
                      InventoryId = prodinv.InventoryId,
                      ProductId = prodinv.ProductId,   
                      Product = prod,
                      Inventory = inventory,
                      InventoryQuantity = prodinv.InventoryQuantity
                    });
                }
            }

            var newProd = new Product
            {
                ProductId = prod.ProductId,
                ProductName = prod.ProductName,
                Quantity = prod.Quantity,
                Price = prod.Price,
                ProductInventories = productInventories
            };
            return await Task.FromResult(newProd);
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) return await Task.FromResult(_products);

            return _products.Where(x => x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public  Task UpdateProductAsync(Product product)
        {
            if (_products.Any(x => x.ProductId != product.ProductId
           && x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }
            var productToUpdate = _products.Find(x => x.ProductId == product.ProductId);
            if (productToUpdate != null)
            {
                productToUpdate.ProductName = product.ProductName;
                productToUpdate.Price = product.Price;
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.ProductInventories = product.ProductInventories;
            }
            return Task.CompletedTask;
        }
    }
}
