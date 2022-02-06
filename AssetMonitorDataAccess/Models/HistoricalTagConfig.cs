using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class HistoricalTagConfig
    {
        public int Id { get; set; }

        public int? AgentTagId { get; set; }
        public AgentTag AgentTag { get; set; }

        public int? SnmpTagId { get; set; }
        public SnmpTag SnmpTag { get; set; }

        [Required]
        public int HistorizationTypeId { get; set; }
        public HistoricalType HistorizationType { get; set; }
    }
}
