using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AssetMonitorSharedGRPC.Server
{
    [DataContract]
    public class AssetsPingDataReply
    {
        [DataMember(Order = 1)]
        public IEnumerable<AssetsPingDataMessage> AssetsData { get; set; }

    }

    [DataContract]
    public class AssetsPingDataRequest
    {
        [DataMember(Order = 1)]
        public int Init { get; set; }
    }

    public struct AssetsPingDataMessage
    {
        public string IpAddress { get; set; }
        public bool PingState { get; set; }
        public long PingResponseTime { get; set; }

        public override string ToString()
        {
            return $"{nameof(IpAddress)}: {IpAddress}, {nameof(PingState)}: {PingState}, {nameof(PingResponseTime)}: {PingResponseTime}";
        }
    }
}
