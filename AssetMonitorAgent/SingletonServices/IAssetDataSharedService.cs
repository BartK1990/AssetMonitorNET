using AssetMonitorSharedGRPC.Agent;
using System.Collections.Generic;

namespace AssetMonitorAgent.SingletonServices
{
    public interface IAssetDataSharedService
    {
        IDictionary<AssetDataItemRequest, object> Data { get; set; }

        void UpdateConfiguration(IEnumerable<AssetDataItemRequest> dataItems);
        void UpdateData();
    }
}