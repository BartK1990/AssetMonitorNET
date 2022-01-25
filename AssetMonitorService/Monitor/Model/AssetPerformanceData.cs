using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model
{
    public class AssetPerformanceData
    {
        public AssetPerformanceData(ICollection<AgentTag> agentTags)
        {
            Data = new Dictionary<AgentTag, TagValue>();
            foreach (var at in agentTags)
            {
                Data.Add(at, new TagValue((TagDataTypeEnum)at.ValueDataTypeId));
            }
        }

        public int Id { get; set; }

        public string IpAddress { get; set; }
        public int? TcpPort { get; set; }

        public IDictionary<AgentTag, TagValue> Data { get; set; }
}
}
