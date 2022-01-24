using AssetMonitorDataAccess.Models.Enums;

namespace AssetMonitorService.Monitor.Model
{
    public class TagValue
    {
        public readonly TagDataTypeEnum DataType;

        public TagValue(TagDataTypeEnum dataType)
        {
            this.DataType = dataType;
        }

        public object Value { get; set; }
    }
}