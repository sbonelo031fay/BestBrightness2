namespace BestBrightnesss.ViewLogic
{
    using System.ComponentModel.DataAnnotations;

    public class InventoryReportView
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock level must be zero or greater.")]
        public int StockLevel { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Restock threshold must be zero or greater.")]
        public int RestockThreshold { get; set; }

        public bool NeedsRestocking => StockLevel < RestockThreshold;
    }

}
