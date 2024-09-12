using BestBrightnesss.ViewLogic;

namespace BestBrightnesss.BusinessLogic.LogicInterface
{
    public interface IReportLogic
    {
        Task<List<DailySalesReportView>> GetAllReportsInvetory();
        Task<List<InventoryReportView>> GetInventoryReport();
        Task<List<DailySalesReportView>> GetReportsByDateRange(DateOnly StartDate, DateOnly EndDate);
        Task<List<InventoryReportView>> GetInventoryReportByDateRange(DateOnly StartDate, DateOnly EndDate);
    }
}
