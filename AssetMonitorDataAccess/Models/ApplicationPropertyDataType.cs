using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class ApplicationPropertyDataType
    {
        public int Id { get; set; }

        [Required]
        public string DataType { get; set; }
    }
}
