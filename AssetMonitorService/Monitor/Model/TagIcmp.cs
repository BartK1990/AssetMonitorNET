using AssetMonitorDataAccess.Models.Enums;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model
{
    public class TagIcmp : IEqualityComparer<TagIcmp>
    {
        public int Id { get; set; }
        public string Tagname { get; set; }
        public double ScaleFactor { get; set; }
        public double ScaleOffset { get; set; }
        public TagDataTypeEnum ValueDataType { get; set; }

        public IcmpTypeEnum IcmpType { get; set; }

        public bool Equals(TagIcmp x, TagIcmp y)
        {
            if (x.Id == y.Id)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(TagIcmp obj)
        {
            return this.Id.GetHashCode();
        }
    }
}
