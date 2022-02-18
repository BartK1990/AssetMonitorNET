using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model
{
    public class AssetPerformanceData
    {
        public AssetPerformanceData(ICollection<AgentTag> tags)
        {
            Data = new Dictionary<AgentTag, TagValue>(new AgentTag());
            foreach (var at in tags)
            {
                Data.Add(at, new TagValue(at.Tagname ,(TagDataTypeEnum)at.ValueDataTypeId, at.ScaleFactor, at.ScaleOffset));
            }
        }

        public int Id { get; set; }

        public string IpAddress { get; set; }
        public int? TcpPort { get; set; }

        public IDictionary<AgentTag, TagValue> Data { get; set; }
    }
}
