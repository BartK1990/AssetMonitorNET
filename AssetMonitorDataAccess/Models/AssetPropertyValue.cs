using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class AssetPropertyValue
    {
        public int Id { get; set; }

        public string Value { get; set; }

        [Required]
        public int AssetPropertyId { get; set; }
        public AssetProperty AssetProperty { get; set; }

        [Required]
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}
