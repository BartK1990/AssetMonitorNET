using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class AgentDataType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string DataType { get; set; }
    }
}
