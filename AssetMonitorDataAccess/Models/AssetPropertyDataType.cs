using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class AssetPropertyDataType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string DataType { get; set; }
    }
}
