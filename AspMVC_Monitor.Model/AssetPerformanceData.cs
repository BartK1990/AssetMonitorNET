namespace AspMVC_Monitor.Model
{
    public class AssetPerformanceData
    {
        public float CpuUsage { get; set; }
        public float MemoryAvailableMB { get; set; }
        public float MemoryTotalMB { get; set; }

        public override string ToString()
        {
            return $"CPU: {CpuUsage}, Memory: {MemoryAvailableMB}, Total memory: {MemoryTotalMB}";
        }
    }
}
