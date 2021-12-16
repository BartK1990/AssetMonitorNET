namespace AssetMonitorAgent.SingletonServices
{
    public interface IAssetDataSharedService
    {
        float CpuUsage { get; set; }
        float MemoryAvailableMB { get; set; }
        float MemoryTotalMB { get; set; }
    }
}