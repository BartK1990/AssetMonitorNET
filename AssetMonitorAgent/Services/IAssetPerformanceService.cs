namespace AssetMonitorAgent.Services
{
    public interface IAssetPerformanceService
    {
        float CpuUsage { get; }
        float MemoryAvailableMB { get; }
        float MemoryTotalMB { get; }
    }
}