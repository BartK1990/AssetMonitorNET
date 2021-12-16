namespace AssetMonitorAgent.SingletonServices
{
    public class AssetDataSharedService : IAssetDataSharedService
    {
        public float CpuUsage { get; set; }
        public float MemoryAvailableMB { get; set; }
        public float MemoryTotalMB { get; set; }
    }
}
