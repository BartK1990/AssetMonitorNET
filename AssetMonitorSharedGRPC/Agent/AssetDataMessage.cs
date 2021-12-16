using System.Runtime.Serialization;

namespace AssetMonitorSharedGRPC.Agent
{
    [DataContract]
    public class AssetDataReply
    {
        [DataMember(Order = 1)]
        public float CpuUsage { get; set; }

        [DataMember(Order = 2)]
        public float MemoryAvailableMB { get; set; }

        [DataMember(Order = 3)]
        public float MemoryTotalMB { get; set; }

        public override string ToString()
        {
            return $"CPU: {CpuUsage}, Memory: {MemoryAvailableMB}, Total memory: {MemoryTotalMB}";
        }
    }

    [DataContract]
    public class AssetDataRequest
    {
        [DataMember(Order = 1)]
        public int Init { get; set; }
    }
}
