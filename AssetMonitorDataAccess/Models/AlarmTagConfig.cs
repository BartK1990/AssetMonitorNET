using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class AlarmTagConfig
    {
        public int Id { get; set; }

        [Required]
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        [Required]
        public int AlarmTypeId { get; set; }
        public AlarmType AlarmType { get; set; }

        [Required] // Time for activation of an alarm in seconds
        public int ActivationTime { get; set; }

        [Required] // Reference value for an alarm
        public string Value { get; set; }

        public string Description { get; set; }
    }
}
