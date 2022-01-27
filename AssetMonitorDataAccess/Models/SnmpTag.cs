using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class SnmpTag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string Tagname { get; set; }

        public double ScaleFactor { get; set; }

        public double ScaleOffset { get; set; }

        [Required]
        [MaxLength(100)]
        public string OID { get; set; }

        [Required]
        public SnmpOperation Operation { get; set; }

        [Required]
        public TagDataType ValueDataType { get; set; }

        [Required]
        public SnmpTagSet SnmpTagSet { get; set; }
    }
}
