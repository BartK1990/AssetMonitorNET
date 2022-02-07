using System.Runtime.Serialization;

namespace AssetMonitorSharedGRPC.Server
{
    [DataContract]
    public class AssetSnmpUpdateCommandReply
    {
        [DataMember(Order = 1)]
        public bool Success { get; set; }

    }

    [DataContract]
    public class AssetSnmpUpdateCommandRequest
    {
        [DataMember(Order = 1)]
        public int AssetId { get; set; }
    }
}
