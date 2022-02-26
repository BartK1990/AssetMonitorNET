using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class AgentTag : IEqualityComparer<AgentTag>
    {
        public int Id { get; set; }
        
        [Required]
        public int AgentDataTypeId { get; set; }
        public AgentDataType AgentDataType { get; set; }

        [MaxLength(200)]
        public string PerformanceCounter { get; set; }

        [MaxLength(200)]
        public string WmiManagementObject { get; set; }

        [MaxLength(256)]
        public string ServiceName { get; set; }

        public TagCommunicationRel TagCommunicationRel { get; set; }

        public bool Equals(AgentTag x, AgentTag y)
        {
            if (x.Id == y.Id)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(AgentTag obj)
        {
            return this.Id.GetHashCode();
        }
    }
}
