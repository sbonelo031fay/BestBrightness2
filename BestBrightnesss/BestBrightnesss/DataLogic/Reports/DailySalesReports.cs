using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BestBrightnesss.DataLogic.Reports
{
    [Keyless]
    public class DailySalesReports
    {
        public DateTime Date { get; set; }
        public decimal TotalSales { get; set; }
        public int ProductsSold { get; set; }
        public string SalesByEmployee { get; set; } = string.Empty;
        public string? ProductName { get; set; } = string.Empty;
    }
}
