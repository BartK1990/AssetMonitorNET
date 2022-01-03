using AssetMonitorService.Monitor.Model;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public interface IAssetPerformanceDataService
    {
        Task UpdateAsset(AssetPerformanceData assetPerformanceData);
    }
}
