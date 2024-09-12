using BestBrightnesss.ViewLogic;

namespace BestBrightnesss.BusinessLogic.LogicInterface
{
    public interface ISalesLogic
    {
        Task RecordSales(SalesView sales, CancellationToken token = default);
    }
}
