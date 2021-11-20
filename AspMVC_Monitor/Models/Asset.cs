namespace AspMVC_Monitor.Models
{
    public class Asset
    {
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public bool pingState { get; set; }
    }
}
