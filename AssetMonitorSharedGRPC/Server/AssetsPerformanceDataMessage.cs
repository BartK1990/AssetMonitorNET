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

    public struct AssetsPerformanceDataMessage
    {
        public string IpAddress { get; set; }
        public int TcpPort { get; set; }
        public float CpuUsage { get; set; }
        public float MemoryAvailableMB { get; set; }
        public float MemoryTotalMB { get; set; }

        public override string ToString()
        {
            return $"{nameof(IpAddress)}: {IpAddress}, {nameof(TcpPort)}: {TcpPort}, {nameof(CpuUsage)}: {CpuUsage}" +
                $", {nameof(MemoryAvailableMB)}: {MemoryAvailableMB}, {nameof(MemoryTotalMB)}: {MemoryTotalMB}";
        }
    }
}
