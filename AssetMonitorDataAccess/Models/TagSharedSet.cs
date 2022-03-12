using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class TagSharedSet
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<TagShared> TagsShared { get; set; }
    }
}
