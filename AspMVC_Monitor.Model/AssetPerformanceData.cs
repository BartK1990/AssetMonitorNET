namespace AspMVC_Monitor.Model
{
    public class AssetPerformanceData
    {
        public float CpuUsage { get; set; }
        public float MemoryAvailable { get; set; }


        public override string ToString()
        {
            return $"CPU: {CpuUsage}, Memory: {MemoryAvailable}";
        }
    }
}
