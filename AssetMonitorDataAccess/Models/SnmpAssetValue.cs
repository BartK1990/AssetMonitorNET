using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class SnmpAssetValue
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int AssetId { get; set; }
        public Asset Asset { get; set; }

        [Required]
        public int SnmpTagId { get; set; }
        public SnmpTag SnmpTag { get; set; }
    }
}
