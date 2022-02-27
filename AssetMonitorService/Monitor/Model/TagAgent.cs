using AssetMonitorDataAccess.Models.Enums;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model
{
    public class TagAgent : IEqualityComparer<TagAgent>
    {
        public int Id { get; set; }
        public string Tagname { get; set; }
        public double ScaleFactor { get; set; }
        public double ScaleOffset { get; set; }
        public TagDataTypeEnum ValueDataType { get; set; }

        public AgentDataTypeEnum AgentDataType { get; set; }
        public string PerformanceCounter { get; set; }
        public string WmiManagementObject { get; set; }
        public string ServiceName { get; set; }

        public bool Equals(TagAgent x, TagAgent y)
        {
            if (x.Id == y.Id)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(TagAgent obj)
        {
            return this.Id.GetHashCode();
        }
    }
}
