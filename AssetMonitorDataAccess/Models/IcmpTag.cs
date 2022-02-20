using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class IcmpTag : IEqualityComparer<IcmpTag>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string Tagname { get; set; }

        public ICollection<HistoricalTagConfig> HistoricalTagConfigs { get; set; }

        public ICollection<AlarmTagConfig> AlarmTagConfigs { get; set; }

        [Required]
        public int ValueDataTypeId { get; set; }
        public TagDataType ValueDataType { get; set; }

        [Required]
        public int IcmpTagSetId { get; set; }
        public IcmpTagSet IcmpTagSet { get; set; }

        public bool Equals(IcmpTag x, IcmpTag y)
        {
            if (x.Id == y.Id)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(IcmpTag obj)
        {
            return this.Id.GetHashCode();
        }
    }
}
