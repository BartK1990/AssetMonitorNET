using AssetMonitorDataAccess.Models.Enums;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model
{
    public class TagSnmp : IEqualityComparer<TagSnmp>
    {
        public int Id { get; set; }
        public string Tagname { get; set; }
        public double ScaleFactor { get; set; }
        public double ScaleOffset { get; set; }
        public TagDataTypeEnum ValueDataType { get; set; }

        public string OID { get; set; }
        public SnmpOperationEnum Operation { get; set; }
        public SnmpCommunicationTypeEnum CommunicationType { get; set; }

        public bool Equals(TagSnmp x, TagSnmp y)
        {
            if (x.Id == y.Id)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(TagSnmp obj)
        {
            return this.Id.GetHashCode();
        }
    }
}
