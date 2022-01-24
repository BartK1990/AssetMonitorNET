using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class AgentTagSet
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string Name { get; set; }

        public ICollection<AgentTag> AgentTag { get; set; }
    }
}
