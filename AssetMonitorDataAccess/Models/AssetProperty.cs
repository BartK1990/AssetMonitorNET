using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class AssetProperty
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ValueDataTypeId { get; set; }
        public AssetPropertyDataType ValueDataType { get; set; }
    }
}
