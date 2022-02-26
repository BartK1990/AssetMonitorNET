using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class TagSet
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
