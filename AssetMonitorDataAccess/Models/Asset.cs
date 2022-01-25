using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class Asset
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)] 
        public string Name { get; set; }

        // 255.255.255.255
        [RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")]
        [Required]
        [MaxLength(15)] 
        public string IpAddress { get; set; }

        public int AssetTypeId { get; set; }
        public AssetType AssetType { get; set; }

        public ICollection<AssetPropertyValue> AssetPropertyValues { get; set; }

        public int? SnmpTagSetId { get; set; }
        public SnmpTagSet SnmpTagSet { get; set; }

        public int? AgentTagSetId { get; set; }
        public AgentTagSet AgentTagSet { get; set; }

        public int? HttpNodeRedTagSetId { get; set; }
        public HttpNodeRedTagSet HttpNodeRedTagSet { get; set; }

    }
}
