using System.Collections.Generic;

namespace AspMVC_Monitor.Models
{
    public class MonitorViewModel
    {
        public ICollection<MonitorTagSharedSet> TagSets { get; set; }
        public ICollection<MonitorTagShared> Tags { get; set; }
        public int? TagSetId { get; set; } = null;
    }
}
