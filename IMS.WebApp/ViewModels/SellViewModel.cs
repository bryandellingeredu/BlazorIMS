using System.ComponentModel.DataAnnotations;
using IMS.CoreBusiness;
using IMS.WebApp.ViewModelsValidations;

namespace IMS.WebApp.ViewModels
{
    public class SellViewModel
    {
        [Required]
        public string SalesOrderNumber { get; set; } = string.Empty;

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "You have to select a product")]
        public int ProductId { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "quantity must be greater than 0")]
        [Sell_EnsureEnoughProductQuantity]
        public int QuantityToSell { get; set; }

        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "quantity must be greater than or equal to 0")]
        public double UnitPrice { get; set; }
        public Product? Product { get; set; }
    }
}
