using AssetMonitorAgent.SingletonServices;
using System.Collections.Generic;

namespace AssetMonitorAgent.Services
{
    public interface IAssetPerformanceService
    {
        IList<object> GetData(IAssetDataSharedService assetDataSharedService);
    }
}