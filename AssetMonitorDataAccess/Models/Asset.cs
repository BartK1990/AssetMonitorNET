using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class Asset
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)] 
        public string Name { get; set; }

        [Required]
        [MaxLength(15)] // 255.255.255.255
        public string IpAddress { get; set; }

        [Required]
        [MaxLength(100)]
        public string Type { get; set; }
    }
}
