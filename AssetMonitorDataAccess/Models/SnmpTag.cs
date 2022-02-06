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
        public bool IsHistorizedLast { get; set; }

        [Required]
        public bool IsHistorizedMax { get; set; }

        [Required]
        public bool IsHistorizedAvg { get; set; }

        [Required]
        public bool IsHistorizedMin { get; set; }

        [Required]
        public string OID { get; set; }

        [Required]
        public int OperationId { get; set; }
        public SnmpOperation Operation { get; set; }

        [Required]
        public int ValueDataTypeId { get; set; }
        public TagDataType ValueDataType { get; set; }

        [Required]
        public int SnmpTagSetId { get; set; }
        public SnmpTagSet SnmpTagSet { get; set; }
    }
}
