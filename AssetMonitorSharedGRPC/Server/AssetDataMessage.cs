using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AssetMonitorSharedGRPC.Server
{
    [DataContract]
    public class AssetsDataReply
    {
        [DataMember(Order = 1)]
        public bool ConfigurationUpdate { get; set; }

        [DataMember(Order = 2)]
        public IEnumerable<AssetDataMessage> AssetsData { get; set; }

    }

    [DataContract]
    public class AssetsDataRequest
    {
        [DataMember(Order = 1)]
        public int Init { get; set; }
    }

    [DataContract]
    public struct AssetDataMessage
    {
        [DataMember(Order = 1)]
        public int AssetId { get; set; }

        [DataMember(Order = 2)]
        public IEnumerable<AssetTagMessage> Tags { get; set; }

    }

    [DataContract]
    public struct AssetTagMessage
    {
        [DataMember(Order = 1)]
        public int TagId { get; set; }

        [DataMember(Order = 2)]
        public byte[] Value { get; set; }

        [DataMember(Order = 3)]
        public bool InAlarm { get; set; }
    }
}
