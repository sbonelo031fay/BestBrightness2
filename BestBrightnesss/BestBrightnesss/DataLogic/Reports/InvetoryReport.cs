using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BestBrightnesss.DataLogic.Reports
{
    [Keyless]
    public class InvetoryReport
    {
        public string Name { get; set; } = string.Empty;
        public int StockLevel { get; set; }
        public int RestockThreshold { get; set; }
        public bool NeedsRestocking => StockLevel < RestockThreshold;
    }
}
