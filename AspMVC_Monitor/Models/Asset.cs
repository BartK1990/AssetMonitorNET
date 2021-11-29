using AspMVC_Monitor.Model;

namespace AspMVC_Monitor.Models
{
    public class Asset
    {
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public bool PingState { get; set; }
        public long PingResponseTime { get; set; }
        public float CpuUsage { get; set; }
        public float MemoryAvailable { get; set; }

        public void AssignPerformanceData(AssetPerformanceData apd)
        {
            this.CpuUsage = apd.CpuUsage;
            this.MemoryAvailable = apd.MemoryAvailable;
        }
    }
}
