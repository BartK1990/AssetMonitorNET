namespace AssetMonitorService.Monitor.Model
{
    public class AssetPerformanceData
    {
        public int Id { get; set; }

        public string IpAddress { get; set; }
        public int TcpPort { get; set; }

        public float CpuUsage { get; set; }
        public float MemoryAvailable { get; set; }
        public float MemoryTotal { get; set; }
    }
}
