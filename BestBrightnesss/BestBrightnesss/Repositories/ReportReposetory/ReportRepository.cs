using BestBrightnesss.DataLogic.Sales;
using BestBrightnesss.DataLogic;
using BestBrightnesss.Repositories.GenericRepository;
using BestBrightnesss.Repositories.RepositoryInterfaces;
using Microsoft.Data.SqlClient;
using BestBrightnesss.DataLogic.Invetory;
using Microsoft.EntityFrameworkCore;
using BestBrightnesss.ViewLogic;
using BestBrightnesss.DataLogic.Reports;
using AutoMapper;
using BestBrightnesss.BusinessLogic.StaticMappings;
using System.Data;

namespace BestBrightnesss.Repositories.ReportReposetory
{
    public class ReportRepository : GenericRepository<DailySalesReports>, iReports
    {
        private DefaultContext _context;
        public ReportRepository(DefaultContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }

        public Task<List<DailySalesReports>> GetAllReportsInvetory(CancellationToken token = default)
        {
            const string query = "EXEC [GetAllReportsInvetory] ";
            var Result = _context.Set<DailySalesReports>().FromSqlRaw(query).ToListAsync(cancellationToken: token);
            return Result;
        }

        public Task<List<InvetoryReport>> GetInventoryReport(CancellationToken token = default)
        {
            const string query = "EXEC [GetInventoryReport] ";
            var Result = _context.Set<InvetoryReport>().FromSqlRaw(query).ToListAsync(cancellationToken: token);
            return Result;
        }

        public async Task<List<InventoryReportView>> GetInventoryReportByDateRange(DateOnly StartDate, DateOnly EndDate, CancellationToken token = default)
        {

            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", StartDate) { SqlDbType = SqlDbType.Date },
                new SqlParameter("@EndDate", EndDate) { SqlDbType = SqlDbType.Date }
            };
            var reports = await _context.DailySalesReports
                .FromSqlRaw("EXEC [GetInventoryReportByDateRange] @StartDate, @EndDate", parameters)
                .ToListAsync(token);
            var viewModels = MappingConfig.Mapper.Map<List<InventoryReportView>>(reports);

            return viewModels;
        }

        public async Task<List<DailySalesReportView>> GetReportsByDateRange(DateOnly startDate, DateOnly endDate, CancellationToken token)
        {
            var start = startDate.ToDateTime(new TimeOnly(0, 0)); 
            var end = endDate.ToDateTime(new TimeOnly(23, 59, 59));

            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", start) { SqlDbType = SqlDbType.Date },
                new SqlParameter("@EndDate", end) { SqlDbType = SqlDbType.Date }
            };
            var reports = await _context.DailySalesReports
                .FromSqlRaw("EXEC [GetReportsByDateRange] @StartDate, @EndDate", parameters)
                .ToListAsync(token);
            var viewModels = MappingConfig.Mapper.Map<List<DailySalesReportView>>(reports);

            return viewModels;
        }

    }
}
