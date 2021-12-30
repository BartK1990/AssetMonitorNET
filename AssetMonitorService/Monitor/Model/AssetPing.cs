namespace AssetMonitorService.Monitor.Model
{
    public class AssetPing
    {
        public string IpAddress { get; set; }

        public bool PingState { get; set; }
        public long PingResponseTime { get; set; }
    }
}
