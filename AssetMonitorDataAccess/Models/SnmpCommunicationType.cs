using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class SnmpCommunicationType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; }
    }
}
