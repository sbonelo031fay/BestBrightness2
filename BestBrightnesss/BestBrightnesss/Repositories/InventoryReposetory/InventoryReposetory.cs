using BestBrightnesss.DataLogic.Sales;
using BestBrightnesss.DataLogic;
using BestBrightnesss.Repositories.GenericRepository;
using BestBrightnesss.Repositories.RepositoryInterfaces;
using Microsoft.Data.SqlClient;
using BestBrightnesss.DataLogic.Invetory;
using Microsoft.EntityFrameworkCore;
using BestBrightnesss.ViewLogic;
using BestBrightnesss.DataLogic.Reports;

namespace BestBrightnesss.Repositories.InventoryReposetory
{
    public class InventoryReposetory : GenericRepository<Invetory>, iInventory
    {
        private DefaultContext _context;
        public InventoryReposetory(DefaultContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }

        public Task<List<Invetory>> GetAllInvetory(CancellationToken token = default)
        {
            const string query = "EXEC [GetAllInvetory] ";
            var Result = _context.Set<Invetory>().FromSqlRaw(query).ToListAsync(cancellationToken: token);
            return Result;
        }
        public Task<List<DailySalesReports>> GetSales(string userEmail, CancellationToken token = default)
        {
            const string query = "EXEC [GetSales] @Email";
            var emailParameter = new SqlParameter("@Email", userEmail);
            var result = _context.Set<DailySalesReports>()
                                 .FromSqlRaw(query, emailParameter)
                                 .ToListAsync(cancellationToken: token);
            return result;
        }

        public async Task<Invetory> InventoryByIdAsync(Guid id, CancellationToken token = default)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProductId", id),
            };
            string query = "EXEC [InventoryByIdAsync] @ProductId";
            var results = await _context.Set<Invetory>().FromSqlRaw(query, parameters).ToListAsync(token);
            return results.FirstOrDefault();
        }

        public async Task UpdateInventoryAsync(InvetoryView product, CancellationToken token)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProductId", product.ProductId),
                new SqlParameter("@Name", product.Name),
                new SqlParameter("@StockLevel", product.StockLevel),
                new SqlParameter("@Price", product.Price),
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC [UpdateInventoryAsync] @ProductId, @Name, @StockLevel, @Price", parameters, token);
            await _context.SaveChangesAsync(token);
        }
    }
}

