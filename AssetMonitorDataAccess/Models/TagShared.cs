using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class TagShared
    {
        public int Id { get; set; }

        [Required]
        public string Tagname { get; set; }

        [Required]
        public string ColumnName { get; set; }

        [Required]
        public int TagSharedSetId { get; set; }
        public TagSharedSet TagSharedSet { get; set; }

        [Required]
        public bool Enable { get; set; } = true;
    }
}
