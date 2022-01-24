using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace WindowsDataLib
{
    public static class AssetPerformance
    {
        private static PerformanceCounter _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private static PerformanceCounter _ramCounter = new PerformanceCounter("Memory", "Available MBytes");

        private static IList<PerformanceCounter> PerformanceCounters = new List<PerformanceCounter>();

        public static float GetCurrentCpuUsage()
        {
            return _cpuCounter.NextValue();
        }

        public static float GetAvailableMemory()
        {
            return _ramCounter.NextValue();
        }

        public static float GetPerformanceCounterValue(string perfCounterName)
        {
            if (string.IsNullOrEmpty(perfCounterName))
            {
                throw new ArgumentNullException();
            }

            var splitted = perfCounterName.Split(';');

            if ((splitted.Length) < 2 || (splitted.Length > 3))
            {
                throw new ArgumentException("Bad format of performance counter name - must be separated with ';' - 2 or 3 parameters");
            }
      
            string categoryName = splitted[0];
            string counterName = splitted[1];
            string instanceName = "";
            if (splitted.Length == 3)
            {
                instanceName = splitted[2];
            }

            PerformanceCounter perfC;

            if (PerformanceCounters.Count > 0)
            {
                perfC = PerformanceCounters
                    .Where(pc => (pc.CategoryName == categoryName) && (pc.CounterName == counterName) && (pc.InstanceName == instanceName))
                    .FirstOrDefault();
                if(perfC != null)
                {
                    return perfC.NextValue();
                }
            }

            perfC = new PerformanceCounter(categoryName, counterName, instanceName);
            PerformanceCounters.Add(perfC);
    
            return perfC.NextValue();
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

        public static double GetWmiValue(string managementObjectName)
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            double managementObjectValue = 0.0;
            foreach (ManagementObject result in results)
            {
                double.TryParse(Convert.ToString(result[managementObjectName]), out managementObjectValue);
            }
            return managementObjectValue;
        }
    }
}
