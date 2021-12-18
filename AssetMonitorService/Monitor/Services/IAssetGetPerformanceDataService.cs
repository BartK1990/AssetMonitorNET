using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public interface IAssetGetPerformanceDataService
    {
        Task GetAssetsDataAsync(string hostname, int tcpPort);
    }
}
