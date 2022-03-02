using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class SnmpAssetValue
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        public int? AssetId { get; set; }
        public Asset Asset { get; set; }

        public int? SnmpTagId { get; set; }
        public Tag SnmpTag { get; set; }
    }
}
