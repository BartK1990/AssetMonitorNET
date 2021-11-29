using System.Diagnostics;

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

        public string getCurrentCpuUsage()
        {
            return _cpuCounter.NextValue() + "%";
        }

        public string getAvailableRAM()
        {
            return _ramCounter.NextValue() + "MB";
        }
    }
}
