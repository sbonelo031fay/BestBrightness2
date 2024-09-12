using BestBrightnesss.ViewLogic;

namespace BestBrightnesss.BusinessLogic.LogicInterface
{
    public interface IInventoryLogic
    {
        Task<List<InvetoryView>> GetAllInvetory();
        Task<List<DailySalesReportView>> GetSales(string userEmail);
        Task<InvetoryView> InventoryByIdAsync(Guid id, CancellationToken token = default);
        Task UpdateInventoryAsync(InvetoryView product, CancellationToken token = default);
    }
}
