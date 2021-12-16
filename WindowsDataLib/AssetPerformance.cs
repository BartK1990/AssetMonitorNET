using System;
using System.Diagnostics;
using System.Management;

namespace WindowsDataLib
{
    public static class AssetPerformance
    {
        private static PerformanceCounter _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private static PerformanceCounter _ramCounter = new PerformanceCounter("Memory", "Available MBytes");

        public static float GetCurrentCpuUsage()
        {
            return _cpuCounter.NextValue();
        }

        public static float GetAvailableMemory()
        {
            return _ramCounter.NextValue();
        }

        public static float GetTotalMemory()
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            double totalMemory = 0.0;
            foreach (ManagementObject result in results)
            {
                double.TryParse(Convert.ToString(result["TotalPhysicalMemory"]), out totalMemory);
            }
            return (float)(totalMemory / (1024 * 1024));
        }
    }
}
