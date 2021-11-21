namespace AspMVC_Monitor.Models
{
    public class Asset
    {
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public bool PingState { get; set; }
        public long PingResponseTime { get; set; }
    }
}
