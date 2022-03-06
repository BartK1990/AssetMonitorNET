using AssetMonitorService.Monitor.Model.Live;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services.Asset.Live
{
    public interface IAssetSnmpDataService : IAssetService<AssetSnmpData>
    {
        Task UpdateAssetOnDemandData(AssetSnmpData assetData);
    }
}
