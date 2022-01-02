using AssetMonitorService.Monitor.Model;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public interface IAssetPingService
    {
        Task UpdateAsset(AssetPing assetPing);
    }
}
