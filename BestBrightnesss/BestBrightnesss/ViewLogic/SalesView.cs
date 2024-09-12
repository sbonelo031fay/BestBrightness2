using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BestBrightnesss.ViewLogic
{
    public class SalesView
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfSale { get; set; } = DateTime.Now;

        [Required]
        [StringLength(255, ErrorMessage = "Salesperson ID cannot exceed 255 characters.")]
        public string SalespersonId { get; set; } = string.Empty;

        // List of products selected for sale
        [Required]
        public List<ProductSaleItem> Products { get; set; } = new List<ProductSaleItem>();

        public class ProductSaleItem
        {
            public bool IsSelected { get; set; }

            [Required]
            public Guid ProductId { get; set; }
            public Decimal Price { get; set; }
            [Required]
            [DataType(DataType.Text)]
            public string ProductName { get; set; } = string.Empty;

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
            public int Quantity { get; set; }
        }
    }
}
