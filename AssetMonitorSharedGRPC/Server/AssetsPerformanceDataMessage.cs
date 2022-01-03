using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AssetMonitorSharedGRPC.Server
{
    [DataContract]
    public class AssetsPerformanceDataReply
    {
        [DataMember(Order = 1)]
        public IEnumerable<AssetsPerformanceDataMessage> AssetsData { get; set; }

    }

    [DataContract]
    public class AssetsPerformanceDataRequest
    {
        [DataMember(Order = 1)]
        public int Init { get; set; }
    }

    [DataContract]
    public struct AssetsPerformanceDataMessage
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string IpAddress { get; set; }

        [DataMember(Order = 3)]
        public int TcpPort { get; set; }

        [DataMember(Order = 4)]
        public float CpuUsage { get; set; }

        [DataMember(Order = 5)]
        public float MemoryAvailableMB { get; set; }

        [DataMember(Order = 6)]
        public float MemoryTotalMB { get; set; }

        public override string ToString()
        {
            return $"{nameof(IpAddress)}: {IpAddress}, {nameof(TcpPort)}: {TcpPort}, {nameof(CpuUsage)}: {CpuUsage}" +
                $", {nameof(MemoryAvailableMB)}: {MemoryAvailableMB}, {nameof(MemoryTotalMB)}: {MemoryTotalMB}";
        }
    }
}
