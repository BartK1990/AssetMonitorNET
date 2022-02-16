using AssetMonitorService.Monitor.Model.Historical;

namespace AssetMonitorService.Monitor.SingletonServices.Historical
{
    public interface IAssetsHistoricalDataSharedService : IAssetsSharedServiceBase<AssetHistoricalData>
    {
        void UpdateAssetsHistoricalValues();
    }
}
