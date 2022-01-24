using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class AssetPropertyValue
    {
        public int Id { get; set; }

        public string Value { get; set; }

        [Required]
        public AssetProperty AssetProperty { get; set; }

        [Required]
        public Asset Asset { get; set; }
    }
}
