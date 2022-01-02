using AssetMonitorService.Monitor.Model;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public interface IAssetGetPerformanceDataService
    {
        Task UpdateAsset(AssetPerformanceData assetPerformanceData);
    }
}
