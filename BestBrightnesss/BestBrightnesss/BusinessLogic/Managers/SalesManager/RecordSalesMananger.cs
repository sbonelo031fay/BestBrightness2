using AutoMapper.Internal.Mappers;
using BestBrightnesss.BusinessLogic.LogicInterface;
using BestBrightnesss.BusinessLogic.StaticMappings;
using BestBrightnesss.DataLogic.Sales;
using BestBrightnesss.Repositories.RepositoryInterfaces;
using BestBrightnesss.ViewLogic;

namespace BestBrightnesss.BusinessLogic.Managers.SalesManager
{
    public class RecordSalesMananger : ISalesLogic
    {
        private readonly iRecordSales _iRecords;
        public RecordSalesMananger(iRecordSales iRecords)
        {
            _iRecords = iRecords;
        }

         public async Task RecordSales(SalesView sales, CancellationToken token = default)
        {
            var DBModel = MappingConfig.Mapper.Map<RecordSales>(sales);
            await _iRecords.RecordSales(DBModel, token);
        }
    }
}