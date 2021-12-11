using AspMVC_Monitor.Model;

namespace AspMVC_Monitor.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public bool PingState { get; set; }
        public long PingResponseTime { get; set; }
        public float CpuUsage { get; set; }
        public float MemoryAvailable { get; set; }
        public float MemoryTotal { get; set; }
        public float MemoryUsage { get 
            { 
                if(MemoryTotal > 0.0F) {
                    return (MemoryTotal - MemoryAvailable)*100 / MemoryTotal;
                }
                return 0.0F;
            } 
        }

        // Read only UI properties
        public string NameUI { get { return Name; } }
        public string IpAddressUI { get { return IpAddress; } }
        public string PingStateUI { get { return PingState.ToString(); } }
        public string PingResponseTimeUI { get { return PingResponseTime.ToString(); } }
        public string CpuUsageUI { get { return CpuUsage.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture); } }
        public string MemoryAvailableUI { get { return MemoryAvailable.ToString("0", System.Globalization.CultureInfo.InvariantCulture); } }
        public string MemoryTotalUI { get { return MemoryTotal.ToString("0", System.Globalization.CultureInfo.InvariantCulture); } }
        public string MemoryUsageUI { get { return MemoryUsage.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture); } }

        public void AssignPerformanceData(AssetPerformanceData apd)
        {
            this.CpuUsage = apd.CpuUsage;
            this.MemoryAvailable = apd.MemoryAvailableMB;
            this.MemoryTotal = apd.MemoryTotalMB;
        }
    }
}
