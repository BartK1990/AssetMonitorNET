using WindowsDataLib;

namespace AssetMonitorAgent.Services
{
    public class AssetPerformanceService : IAssetPerformanceService
    {
        public float CpuUsage
        {
            get
            {
                return AssetPerformance.GetCurrentCpuUsage();
            }
        }

        public float MemoryAvailableMB
        {
            get
            {
                return AssetPerformance.GetAvailableMemory();
            }
        }

        public float MemoryTotalMB
        {
            get
            {
                return AssetPerformance.GetTotalMemory();
            }
        }
    }
}
