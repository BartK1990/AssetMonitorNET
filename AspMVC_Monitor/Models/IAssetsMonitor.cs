using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Models
{
    public interface IAssetsMonitor
    {
        List<AssetLiveData> AssetsList { get; set; }

        void UpdateAssetPing();
        Task UpdateAssetPingAsync();

        void UpdateAssetPerformance();
        Task UpdateAssetPerformanceAsync();

        void UpdateAssetsList();
        Task UpdateAssetsListAsync();
    }
}
