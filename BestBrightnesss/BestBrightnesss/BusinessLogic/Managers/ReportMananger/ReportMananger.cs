using BestBrightnesss.BusinessLogic.LogicInterface;
using BestBrightnesss.BusinessLogic.StaticMappings;
using BestBrightnesss.Repositories.RepositoryInterfaces;
using BestBrightnesss.ViewLogic;

namespace BestBrightnesss.BusinessLogic.Managers.ReportMananger
{
    public class ReportMananger : IReportLogic
    {
        private readonly iReports _iReports;
        public ReportMananger(iReports iReports)
        {
            _iReports = iReports;
        }

        public async Task<List<DailySalesReportView>> GetAllReportsInvetory()
        {
            var inventory = await _iReports.GetAllReportsInvetory();
            return MappingConfig.Mapper.Map<List<DailySalesReportView>>(inventory);
        }

        public async Task<List<InventoryReportView>> GetInventoryReport()
        {
            var inventory = await _iReports.GetInventoryReport();
            return MappingConfig.Mapper.Map<List<InventoryReportView>>(inventory);
        }

        public async Task<List<InventoryReportView>> GetInventoryReportByDateRange(DateOnly startDate, DateOnly endDate)
        {
            var inventory = await _iReports.GetInventoryReportByDateRange(startDate, endDate);
            return MappingConfig.Mapper.Map<List<InventoryReportView>>(inventory);
        }

        public async Task<List<DailySalesReportView>> GetReportsByDateRange(DateOnly startDate, DateOnly endDate)
        {
            var inventory = await _iReports.GetReportsByDateRange(startDate, endDate);
            return MappingConfig.Mapper.Map<List<DailySalesReportView>>(inventory);
        }


    }
}
