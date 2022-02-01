using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model
{
    public class AssetSnmpData
    {
        public AssetSnmpData(ICollection<SnmpTag> agentTags)
        {
            Data = new Dictionary<SnmpTag, TagValue>();
            foreach (var at in agentTags)
            {
                Data.Add(at, new TagValue((TagDataTypeEnum)at.ValueDataTypeId, at.ScaleFactor, at.ScaleOffset));
            }
        }

        public int Id { get; set; }

        public string IpAddress { get; set; }
        public string Community { get; set; }
        public int UdpPort { get; set; }
        public int Timeout { get; set; }
        public int Retries { get; set; }
        public SnmpVersionEnum Version { get; set; }

        public IDictionary<SnmpTag, TagValue> Data { get; set; }
    }
}