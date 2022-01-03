namespace AssetMonitorService.Monitor.Model
{
    public class AssetPing
    {
        public int Id { get; set; }

        public string IpAddress { get; set; }

        public bool PingState { get; set; }
        public long PingResponseTime { get; set; }
    }
}
