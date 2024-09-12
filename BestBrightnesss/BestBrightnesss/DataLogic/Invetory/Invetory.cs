using Microsoft.EntityFrameworkCore;

namespace BestBrightnesss.DataLogic.Invetory
{
    [Keyless]
    public class Invetory
    {
        public Guid ProductId { get; set; }  
        public string Name { get; set; } = string.Empty;
        public int StockLevel { get; set; }
        public Decimal Price { get; set; }
    }
}
