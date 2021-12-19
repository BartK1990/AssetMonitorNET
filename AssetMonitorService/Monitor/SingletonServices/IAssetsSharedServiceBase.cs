using System.Collections.Generic;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public interface IAssetsSharedServiceBase<T>
    {
        List<T> AssetsData { get; set; }

        void UpdateAssetsListBase();
    }
}
