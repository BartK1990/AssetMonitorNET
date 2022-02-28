using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;

namespace AssetMonitorService.Monitor.Model.TagConfig
{
    public class TagAgent : TagConfigBase
    {
        public TagAgent()
        {
        }

        public TagAgent(Tag tag) : base(tag)
        {
            if(tag.TagCommunicationRel?.AgentTag != null)
            {
                AgentDataType = (AgentDataTypeEnum)tag.TagCommunicationRel.AgentTag.AgentDataTypeId;
                PerformanceCounter = tag.TagCommunicationRel.AgentTag.PerformanceCounter;
                WmiManagementObject = tag.TagCommunicationRel.AgentTag.WmiManagementObject;
                ServiceName = tag.TagCommunicationRel.AgentTag.ServiceName;
            }
        }

        public AgentDataTypeEnum AgentDataType { get; set; }
        public string PerformanceCounter { get; set; }
        public string WmiManagementObject { get; set; }
        public string ServiceName { get; set; }
    }
}
