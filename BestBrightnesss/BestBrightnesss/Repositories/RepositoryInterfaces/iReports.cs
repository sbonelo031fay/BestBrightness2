using BestBrightnesss.DataLogic.Invetory;
using BestBrightnesss.DataLogic.Reports;
using BestBrightnesss.Repositories.Interfaces;
using BestBrightnesss.ViewLogic;

namespace BestBrightnesss.Repositories.RepositoryInterfaces
{
    public interface iReports : iGenericRepository<DailySalesReports>
    {
        Task<List<DailySalesReports>> GetAllReportsInvetory(CancellationToken token = default);
        Task<List<InvetoryReport>> GetInventoryReport (CancellationToken token = default);
        Task<List<DailySalesReportView>> GetReportsByDateRange(DateOnly StartDate, DateOnly EndDate, CancellationToken token = default);
        Task<List<InventoryReportView>> GetInventoryReportByDateRange(DateOnly StartDate, DateOnly EndDate, CancellationToken token = default);
    }
}
