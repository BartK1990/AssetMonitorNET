using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public interface IAssetPingService
    {
        Task PingHostAsync(string hostname);
    }
}
