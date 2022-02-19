using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices.Historical
{
    public interface IHistoricalTablesSharedService
    {
        Task DatabaseStructureUpdateAsync();
        Task InsertTimedDataForAllAssetsAsync(string timestamp);
    }
}
