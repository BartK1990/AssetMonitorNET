using AssetMonitorService.Monitor.Model.Live;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public interface IAssetPerformanceDataService : IAssetService<AssetPerformanceData>
    {
        Task UpdateAsset(AssetPerformanceData asset, int scanTime);
    }
}
