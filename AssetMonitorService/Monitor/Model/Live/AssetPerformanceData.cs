using AssetMonitorService.Monitor.Model.TagConfig;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model.Live
{
    public class AssetPerformanceData
    {
        public AssetPerformanceData(ICollection<TagAgent> tags)
        {
            Data = new Dictionary<TagAgent, TagValue>(new TagAgent());
            foreach (var tag in tags)
            {
                Data.Add(tag, new TagValue(tag.Tagname , tag.ValueDataType, tag.ScaleFactor, tag.ScaleOffset));
            }
        }

        public int Id { get; set; }

        public string IpAddress { get; set; }
        public int? TcpPort { get; set; }

        public IDictionary<TagAgent, TagValue> Data { get; set; }
    }
}
