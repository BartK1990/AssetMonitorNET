using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class Asset
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(15)] // 255.255.255.255
        public string IpAddress { get; set; }

        public string Type { get; set; }
    }
}
