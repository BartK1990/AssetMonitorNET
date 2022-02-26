using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetMonitorDataAccess.Models
{
    public class Tag : IEqualityComparer<Tag>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string Tagname { get; set; }

        public double ScaleFactor { get; set; }

        public double ScaleOffset { get; set; }

        [Required]
        public int ValueDataTypeId { get; set; }
        public TagDataType ValueDataType { get; set; }

        public ICollection<HistoricalTagConfig> HistoricalTagConfigs { get; set; }

        public ICollection<AlarmTagConfig> AlarmTagConfigs { get; set; }

        [Required]
        public int TagSetId { get; set; }
        public TagSet TagSet { get; set; }

        [Required]
        public int TagCommunicationRelId { get; set; }
        public TagCommunicationRel TagCommunicationRel { get; set; }

        public bool Equals(Tag x, Tag y)
        {
            if (x.Id == y.Id)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(Tag obj)
        {
            return this.Id.GetHashCode();
        }
    }
}
