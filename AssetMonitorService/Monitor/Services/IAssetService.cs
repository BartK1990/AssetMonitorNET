using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public interface IAssetService<T>
    {
        Task UpdateAsset(T asset);
    }
}
