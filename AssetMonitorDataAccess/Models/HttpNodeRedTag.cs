using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class HttpNodeRedTag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string Tagname { get; set; }

        public double ScaleFactor { get; set; }

        public double ScaleOffset { get; set; }

        [MaxLength(200)]
        public string HttpTag { get; set; }

        [Required]
        public TagDataType ValueDataType { get; set; }

        [Required]
        public HttpNodeRedTagSet HttpNodeRedTagSet { get; set; }
    }
}
