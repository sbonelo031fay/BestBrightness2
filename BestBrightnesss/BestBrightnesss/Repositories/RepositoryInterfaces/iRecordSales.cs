using BestBrightnesss.DataLogic.Sales;
using BestBrightnesss.Repositories.Interfaces;
using BestBrightnesss.ViewLogic;

namespace BestBrightnesss.Repositories.RepositoryInterfaces
{
    public interface iRecordSales : iGenericRepository<RecordSales>
    {
        Task RecordSales(RecordSales sales, CancellationToken token = default);
    }
}
