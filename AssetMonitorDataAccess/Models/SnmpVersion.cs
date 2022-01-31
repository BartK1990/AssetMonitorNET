using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class SnmpVersion
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Version { get; set; }
    }
}
