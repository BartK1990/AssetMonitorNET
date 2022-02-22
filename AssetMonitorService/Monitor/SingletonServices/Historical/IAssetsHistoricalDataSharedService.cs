using AssetMonitorService.Monitor.Model.Historical;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices.Historical
{
    public interface IAssetsHistoricalDataSharedService : IAssetsSharedServiceBase<AssetHistoricalData>
    {
        void UpdateAssetsHistoricalValues();
        Task<bool> UpdateAssetActualSnmpValuesByIdAsync(int assetId);
    }
}
