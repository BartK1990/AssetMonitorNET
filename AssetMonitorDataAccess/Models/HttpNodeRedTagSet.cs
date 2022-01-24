using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class HttpNodeRedTagSet
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string Name { get; set; }

        public ICollection<HttpNodeRedTag> HttpNodeRedTag { get; set; }
    }
}
