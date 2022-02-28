using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model.TagConfig
{
    public class TagConfigBase : IEqualityComparer<TagConfigBase>
    {
        public TagConfigBase()
        {
        }

        public TagConfigBase(Tag tag)
        {
            Id = tag.Id;
            Tagname = tag.Tagname;
            ScaleFactor = tag.ScaleFactor;
            ScaleOffset = tag.ScaleOffset;
            ValueDataType = (TagDataTypeEnum)tag.ValueDataTypeId;
        }

        public int Id { get; set; }
        public string Tagname { get; set; }
        public double ScaleFactor { get; set; }
        public double ScaleOffset { get; set; }
        public TagDataTypeEnum ValueDataType { get; set; }

        public bool Equals(TagConfigBase x, TagConfigBase y)
        {
            if (x.Id == y.Id)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(TagConfigBase obj)
        {
            return this.Id.GetHashCode();
        }
    }
}