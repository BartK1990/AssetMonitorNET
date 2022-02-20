using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class SnmpTagSet
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int VersionId { get; set; }
        public SnmpVersion Version { get; set; }

        public ICollection<SnmpTag> SnmpTag { get; set; }
    }
}
