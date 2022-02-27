using AssetMonitorSharedGRPC.Agent;
using System.Collections.Generic;

namespace AssetMonitorAgent.SingletonServices
{
    public interface IAssetDataSharedService
    {
        IDictionary<AssetDataItemRequest, object> Data { get; set; }
        public int ScanTime { get; }

        void UpdateConfiguration(IEnumerable<AssetDataItemRequest> dataItems, int scanTime);
        void UpdateData();
    }
}