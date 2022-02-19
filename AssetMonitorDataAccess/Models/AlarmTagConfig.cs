using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class AlarmTagConfig
    {
        public int Id { get; set; }

        [Required]
        public bool Ping { get; set; }

        public int? AgentTagId { get; set; }
        public AgentTag AgentTag { get; set; }

        public int? SnmpTagId { get; set; }
        public SnmpTag SnmpTag { get; set; }

        [Required]
        public int AlarmTypeId { get; set; }
        public AlarmType AlarmType { get; set; }

        [Required] // Time for activation of an alarm in seconds
        public int ActivationTime { get; set; }

        [Required] // Reference value for an alarm
        public string Value { get; set; }
    }
}
