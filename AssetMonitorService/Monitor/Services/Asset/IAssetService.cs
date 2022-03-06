using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services.Asset
{
    public interface IAssetService<T>
    {
        Task UpdateAsset(T asset);
    }
}
