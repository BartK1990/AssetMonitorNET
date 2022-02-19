using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class AlarmType
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
