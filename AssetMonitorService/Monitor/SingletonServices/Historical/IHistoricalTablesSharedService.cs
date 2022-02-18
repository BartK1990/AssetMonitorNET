using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices.Historical
{
    public interface IHistoricalTablesSharedService
    {
        Task DatabaseStructureUpdate();
        Task InsertTimedDataForAllAssets(string timestamp);
    }
}
