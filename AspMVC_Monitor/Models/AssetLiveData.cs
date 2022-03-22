using System.Collections.Generic;

namespace AspMVC_Monitor.Models
{
    public class AssetLiveData
    {
        public AssetLiveData(int id, string name, string ipAddress, IDictionary<int, TagLiveValue> tags)
        {
            Id = id;
            Name = name;
            IpAddress = ipAddress;
            Tags = tags;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }

        public IDictionary<int, TagLiveValue> Tags { get; set; }
    }
}
