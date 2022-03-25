using System.Collections.Generic;
using System.Linq;

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

        public bool InAlarm { get 
            {
                if(!Tags?.Any() ?? true)
                {
                    return false;
                }

                foreach (var tag in Tags.Values)
                {
                    if (tag.InAlarm)
                    {
                        return true;
                    }
                }

                return false;
            } 
        }

        public IDictionary<int, TagLiveValue> Tags { get; set; }
        public IDictionary<int, IDictionary<int, int>> TagsIdForSharedTagSets { get; set; }
    }
}
