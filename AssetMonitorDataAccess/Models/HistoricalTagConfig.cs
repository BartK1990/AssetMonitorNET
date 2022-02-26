using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class HistoricalTagConfig
    {
        public int Id { get; set; }

        [Required]
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        [Required]
        public int HistorizationTypeId { get; set; }
        public HistoricalType HistorizationType { get; set; }
    }
}
