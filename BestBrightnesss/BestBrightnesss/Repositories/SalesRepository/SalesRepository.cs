using BestBrightnesss.BusinessLogic.LogicInterface;
using BestBrightnesss.DataLogic;
using BestBrightnesss.DataLogic.Sales;
using BestBrightnesss.Repositories.GenericRepository;
using BestBrightnesss.Repositories.RepositoryInterfaces;
using BestBrightnesss.ViewLogic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BestBrightnesss.Repositories.SalesRepository
{
    public class SalesRepository : GenericRepository<RecordSales>, iRecordSales
    {
        private DefaultContext _context;
        public SalesRepository(DefaultContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }

        public async Task RecordSales(RecordSales sales, CancellationToken token = default)
        {
            // Convert product details to a comma-separated string
            var productDetails = sales.Products
                .Select(p => $"{p.ProductId.ToString()}:{p.Quantity}")
                .ToArray();

            var productDetailString = string.Join(",", productDetails);

            // Create SQL parameters for the stored procedure
            SqlParameter[] procedureParameters =
            {
                new SqlParameter("@SalespersonId", sales.SalespersonId),
                new SqlParameter("@DateOfSale", sales.DateOfSale),
                new SqlParameter("@ProductDetails", productDetailString)
            };

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync("EXEC [RecordSales] @SalespersonId, @DateOfSale, @ProductDetails", procedureParameters, token);
            await _context.SaveChangesAsync(token);
        }

    }
}