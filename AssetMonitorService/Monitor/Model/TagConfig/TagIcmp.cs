using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;

namespace AssetMonitorService.Monitor.Model.TagConfig
{
    public class TagIcmp : TagConfigBase
    {
        public TagIcmp()
        {
        }

        public TagIcmp(Tag tag) : base(tag)
        {
            if (tag.TagCommunicationRel?.IcmpTag != null)
            {
                IcmpType = (IcmpTypeEnum)tag.TagCommunicationRel.IcmpTag.IcmpTypeId;
            }
        }

        public IcmpTypeEnum IcmpType { get; set; }
    }
}
