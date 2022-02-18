using AssetMonitorDataAccess.Models.Enums;
using System;

namespace AssetMonitorService.Monitor.Model
{
    public class TagValue
    {
        public readonly string Tagname;

        public readonly TagDataTypeEnum DataType;

        public TagValue(string tagname, TagDataTypeEnum dataType, double scaleFactor, double scaleOffset)
        {
            this.Tagname = tagname;
            this.DataType = dataType;
            this.ScaleFactor = scaleFactor;
            this.ScaleOffset = scaleOffset;
        }

        public readonly double ScaleFactor;
        public readonly double ScaleOffset;

        private object _value;
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                switch (DataType)
                {
                    case TagDataTypeEnum.Integer:
                        try
                        {
                            _value = Convert.ToDouble(value) * ScaleFactor + ScaleOffset;
                        }
                        catch { }
                        break;
                    case TagDataTypeEnum.Float:
                        try
                        {
                            _value = Convert.ToDouble(value) * ScaleFactor + ScaleOffset;
                        }
                        catch { }
                        break;
                    case TagDataTypeEnum.Double:
                        try
                        {
                            _value = Convert.ToDouble(value) * ScaleFactor + ScaleOffset;
                        }
                        catch { }
                        break;
                    default:
                        _value = value;
                        break;
                }
            }
        }
    }
}