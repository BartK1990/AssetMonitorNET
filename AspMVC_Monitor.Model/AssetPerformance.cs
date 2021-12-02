using System.Linq;
using System.Diagnostics;
using System.Management;
using System;

namespace AspMVC_Monitor.Model
{
    public class AssetPerformance
    {
        private readonly PerformanceCounter _cpuCounter;
        private readonly PerformanceCounter _ramCounter;

        public AssetPerformance()
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public AssetPerformanceData GetPerformanceData()
        {
            var apd = new AssetPerformanceData()
            {
                CpuUsage = _cpuCounter.NextValue(),
                MemoryAvailableMB = _ramCounter.NextValue(),
                MemoryTotalMB = (float)(GetTotalMemory()/(1024*1024))
            };
            return apd;
        }

        public string GetCurrentCpuUsage()
        {
            return _cpuCounter.NextValue() + "%";
        }

        public string GetAvailableMemory()
        {
            return _ramCounter.NextValue() + "MB";
        }

        public double GetTotalMemory()
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            double totalMemory = 0.0;
            foreach (ManagementObject result in results)
            {
                //Console.WriteLine("Total Visible Memory: {0} KB", result["TotalVisibleMemorySize"]);
                //Console.WriteLine("Free Physical Memory: {0} KB", result["FreePhysicalMemory"]);
                //Console.WriteLine("Total Virtual Memory: {0} KB", result["TotalVirtualMemorySize"]);
                //Console.WriteLine("Free Virtual Memory: {0} KB", result["FreeVirtualMemory"]);
                double.TryParse(Convert.ToString(result["TotalPhysicalMemory"]), out totalMemory);
            }
            return totalMemory;
        }
    }
}
