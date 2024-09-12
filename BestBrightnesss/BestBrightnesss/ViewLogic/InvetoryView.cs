namespace BestBrightnesss.ViewLogic
{
    public class InvetoryView
    {
        public Guid? ProductId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public int StockLevel { get; set; }
        public Decimal Price { get; set; }
    }
}
