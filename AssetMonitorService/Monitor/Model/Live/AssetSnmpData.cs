using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Monitor.Model.TagConfig;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model.Live
{
    public class AssetSnmpData
    {
        public AssetSnmpData(ICollection<TagSnmp> tags)
        {
            Data = new Dictionary<TagSnmp, TagValue>(new TagSnmp());
            foreach (var tag in tags)
            {
                Data.Add(tag, new TagValue(tag.Tagname, tag.ValueDataType, tag.ScaleFactor, tag.ScaleOffset));
            }
        }

        public int Id { get; set; }

        public string IpAddress { get; set; }
        public string Community { get; set; }
        public int UdpPort { get; set; }
        public int Timeout { get; set; }
        public int Retries { get; set; }
        public SnmpVersionEnum Version { get; set; }

        public IDictionary<TagSnmp, TagValue> Data { get; set; }
    }
}