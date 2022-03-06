using AssetMonitorService.Monitor.Model.Live;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services.Asset.Live
{
    public interface IAssetPerformanceDataService : IAssetService<AssetPerformanceData>
    {
        Task UpdateAsset(AssetPerformanceData asset, int scanTime);
    }
}
