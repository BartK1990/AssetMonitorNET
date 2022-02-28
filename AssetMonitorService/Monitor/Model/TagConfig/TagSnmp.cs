using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;

namespace AssetMonitorService.Monitor.Model.TagConfig
{
    public class TagSnmp : TagConfigBase
    {
        public TagSnmp()
        {
        }

        public TagSnmp(Tag tag) : base(tag)
        {
            if (tag.TagCommunicationRel?.SnmpTag != null)
            {
                OID = tag.TagCommunicationRel.SnmpTag.OID;
                Operation = (SnmpOperationEnum)tag.TagCommunicationRel.SnmpTag.OperationId;
                CommunicationType = (SnmpCommunicationTypeEnum)tag.TagCommunicationRel.SnmpTag.SnmpCommunicationTypeId;
            }
        }

        public string OID { get; set; }
        public SnmpOperationEnum Operation { get; set; }
        public SnmpCommunicationTypeEnum CommunicationType { get; set; }
    }
}
