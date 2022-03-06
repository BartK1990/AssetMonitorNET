using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class ApplicationProperty
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public ApplicationPropertyValue ApplicationPropertyValue { get; set; }

        [Required]
        public int ValueDataTypeId { get; set; }
        public ApplicationPropertyDataType ValueDataType { get; set; }
    }
}
