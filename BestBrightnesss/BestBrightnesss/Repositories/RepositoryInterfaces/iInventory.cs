using BestBrightnesss.DataLogic.Invetory;
using BestBrightnesss.DataLogic.Reports;
using BestBrightnesss.DataLogic.Sales;
using BestBrightnesss.Repositories.Interfaces;
using BestBrightnesss.ViewLogic;

namespace BestBrightnesss.Repositories.RepositoryInterfaces
{
    public interface iInventory : iGenericRepository<Invetory>
    {
        Task<List<Invetory>> GetAllInvetory(CancellationToken token = default);
        Task<Invetory> InventoryByIdAsync(Guid id, CancellationToken token = default);
        Task<List<DailySalesReports>> GetSales(string userEmail, CancellationToken token = default);
        Task UpdateInventoryAsync(InvetoryView product, CancellationToken token);
    }
}
