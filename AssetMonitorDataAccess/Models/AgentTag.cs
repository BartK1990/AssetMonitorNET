using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class AgentTag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string Tagname { get; set; }

        [Required]
        public AgentDataType AgentDataType { get; set; }

        [MaxLength(200)]
        public string PerformanceCounter { get; set; }

        [MaxLength(200)]
        public string WmiManagementObject { get; set; }

        [MaxLength(256)]
        public string ServiceName { get; set; }

        [Required]
        public TagDataType ValueDataType { get; set; }

        [Required]
        public AgentTagSet AgentTagSet { get; set; }
    }
}
