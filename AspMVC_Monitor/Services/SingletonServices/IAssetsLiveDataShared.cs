using AspMVC_Monitor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Services.SingletonServices
{
    public interface IAssetsLiveDataShared
    {
        List<AssetLiveData> AssetsData { get; }
        Task UpdateAssetsLiveData();
        Task UpdateAssetsSharedTagSets();
        Task UpdateAssetSnmpData(int assetId);
    }
}
