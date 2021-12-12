using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Models
{
    public interface IAssetHolder
    {
        List<AssetMonitor> AssetList { get; set; }

        void AddAsset(string name, string ipAddress);

        void UpdateAssetPing();
        Task UpdateAssetPingAsync();

        void UpdateAssetPerformance();
        Task UpdateAssetPerformanceAsync();

        void UpdateAssetsList();
        Task UpdateAssetsListAsync();
    }
}
