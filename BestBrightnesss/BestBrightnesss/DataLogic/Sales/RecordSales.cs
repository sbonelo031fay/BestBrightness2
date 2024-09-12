using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BestBrightnesss.DataLogic.Sales
{
    [Keyless]
    public class RecordSales
    {
        public DateTime DateOfSale { get; set; } = DateTime.Now;

        [Required]
        public string SalespersonId { get; set; } = string.Empty;

        public List<ProductSaleItem> Products { get; set; } = new List<ProductSaleItem>();

        public class ProductSaleItem
        {
            public bool IsSelected { get; set; }
            public Guid ProductId { get; set; }
            public Decimal Price { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public int Quantity { get; set; }
        }
    }
}
