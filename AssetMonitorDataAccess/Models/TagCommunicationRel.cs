using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class TagCommunicationRel
    {
        public int Id { get; set; }

        public Tag Tag { get; set; }

        [Required]
        public bool IcmpTag { get; set; }

        public int? AgentTagId { get; set; }
        public AgentTag AgentTag { get; set; }

        public int? SnmpTagId { get; set; }
        public SnmpTag SnmpTag { get; set; }
    }
}
