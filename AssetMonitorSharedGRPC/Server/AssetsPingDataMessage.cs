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

    [DataContract]
    public struct AssetsPingDataMessage
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string IpAddress { get; set; }

        [DataMember(Order = 3)]
        public bool PingState { get; set; }

        [DataMember(Order = 4)]
        public long PingResponseTime { get; set; }

        public override string ToString()
        {
            return $"{nameof(IpAddress)}: {IpAddress}, {nameof(PingState)}: {PingState}, {nameof(PingResponseTime)}: {PingResponseTime}";
        }
    }
}
