using AutoMapper.Internal.Mappers;
using BestBrightnesss.BusinessLogic.LogicInterface;
using BestBrightnesss.BusinessLogic.StaticMappings;
using BestBrightnesss.DataLogic.Sales;
using BestBrightnesss.Repositories.RepositoryInterfaces;
using BestBrightnesss.ViewLogic;

namespace BestBrightnesss.BusinessLogic.Managers.InventoryMananger
{
    public class InvetoryMananger : IInventoryLogic
    {
        private readonly iInventory _inventory;
        public InvetoryMananger(iInventory inventory)
        {
            _inventory = inventory;
        }

        public async Task<List<InvetoryView>> GetAllInvetory()
        {
            var inventory = await _inventory.GetAllInvetory();
            return MappingConfig.Mapper.Map<List<InvetoryView>>(inventory);
        }

        public async Task<List<DailySalesReportView>> GetSales(string userEmail)
        {
            var sales = await _inventory.GetSales(userEmail);
            return MappingConfig.Mapper.Map<List<DailySalesReportView>>(sales);
        }

        public async Task<InvetoryView> InventoryByIdAsync(Guid id, CancellationToken token = default)
        {
            var model = await _inventory.InventoryByIdAsync(id, token);
            var inventoryView = MappingConfig.Mapper.Map<InvetoryView>(model);
            return inventoryView;
        }
        public async Task UpdateInventoryAsync(InvetoryView product, CancellationToken token = default)
        {
            await _inventory.UpdateInventoryAsync(product, token);
        }
    }
}
