using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public interface IAssetGetDataService
    {
        Task<string> GetAssetsDataAsync();
    }
}
