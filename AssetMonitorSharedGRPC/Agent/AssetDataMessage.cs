using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AssetMonitorSharedGRPC.Agent
{
    [DataContract]
    public class AssetDataReply
    {
        [DataMember(Order = 1)]
        public IEnumerable<AssetDataItemReply> Data { get; set; }
    }

    [DataContract]
    public class AssetDataRequest
    {
        public AssetDataRequest(int scanTime, IEnumerable<AssetDataItemRequest> tags)
        {
            this.ScanTime = scanTime;
            this.Tags = tags;
        }

        [DataMember(Order = 1)]
        public IEnumerable<AssetDataItemRequest> Tags { get; set; }

        [DataMember(Order = 2)]
        public int ScanTime { get; set; }
    }

    [DataContract]
    public class AssetDataItemReply
    {
        [DataMember(Order = 1)]
        public byte[] ByteArray { get; set; }
    }

    [DataContract]
    public class AssetDataItemRequest
    {
        [DataMember(Order = 1)]
        public int DataType { get; set; }

        [DataMember(Order = 2)]
        public int AgentDataTypeId { get; set; }

        [DataMember(Order = 3)]
        public string PerformanceCounter { get; set; }

        [DataMember(Order = 4)]
        public string WmiManagementObject { get; set; }

        [DataMember(Order = 5)]
        public string ServiceName { get; set; }
    }
}
