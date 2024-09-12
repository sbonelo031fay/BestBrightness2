using System;
using System.ComponentModel.DataAnnotations;

namespace BestBrightnesss.ViewLogic
{
    public class DailySalesReportView
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalSales { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Number of products sold must be zero or greater.")]
        public int ProductsSold { get; set; }

        [Required]
        public string SalesByEmployee { get; set; } = string.Empty;
        public string? ProductName { get; set; } = string.Empty;
    }
}
