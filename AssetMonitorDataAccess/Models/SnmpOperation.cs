using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class SnmpOperation
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Operation { get; set; }
    }
}
