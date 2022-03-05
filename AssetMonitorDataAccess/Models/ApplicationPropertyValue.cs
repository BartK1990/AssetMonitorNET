using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class ApplicationPropertyValue
    {
        public int Id { get; set; }

        public string Value { get; set; }

        [Required]
        public int ApplicationPropertyId { get; set; }
        public ApplicationProperty ApplicationProperty { get; set; }
    }
}
