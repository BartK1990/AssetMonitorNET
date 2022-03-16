using System.Collections.Generic;

namespace AspMVC_Monitor.Models
{
    public class AssetLiveData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }

        public ICollection<TagLiveValue> Tags { get; set; }
    }
}
