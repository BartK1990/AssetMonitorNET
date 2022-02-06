using System.ComponentModel.DataAnnotations;

namespace AssetMonitorHistoryDataAccess.Models
{
    public class HistoricalDataTable
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
