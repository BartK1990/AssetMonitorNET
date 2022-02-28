using AssetMonitorDataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetMonitorService.Monitor.Model.Historical
{
#nullable enable
    public class TagHistoricalValue : TagValueBase
    {
        public readonly TagDataTypeEnum DataType;
        public readonly int WindowSize;

        public TagHistoricalValue(string tagname, TagDataTypeEnum dataType, int windowSize)
            : base(tagname: tagname)
        {
            this.DataType = dataType;
            this.WindowSize = windowSize;
            ValueBuffer = new Queue<object?>();
        }

        public Queue<object?> ValueBuffer { get; private set; }

        private object? _valueLast;
        public object? ValueLast 
        { 
            get 
            {
                return _valueLast;
            }
            private set 
            {
                _valueLast = value;
            } 
        }

        public object? ValueMax 
        {
            get
            {
                return GetSpecificType(CalculationTypeEnum.Max);
            }
        }
        public object? ValueAvg
        {
            get
            {
                return GetSpecificType(CalculationTypeEnum.Avg);
            }
        }
        public object? ValueMin
        {
            get
            {
                return GetSpecificType(CalculationTypeEnum.Min);
            }
        }

        private object? GetSpecificType(CalculationTypeEnum calculationType)
        {
            if ((DataType != TagDataTypeEnum.Integer &&
                DataType != TagDataTypeEnum.Long &&
                DataType != TagDataTypeEnum.Float &&
                DataType != TagDataTypeEnum.Double))
            {
                return ValueLast;
            }

            if (!(ValueBuffer?.Any()) ?? true)
            {
                return ValueLast;
            }
#pragma warning disable CS8604 // Possible null reference argument.
            object? result = GetCalculatedValue(calculationType, ValueBuffer); // Null check should be performed above
#pragma warning restore CS8604 // Possible null reference argument.

            if (result == null)
            {
                return result;
            }

            switch (DataType)
            {
                case TagDataTypeEnum.Integer:
                    return Convert.ToInt32(result);
                case TagDataTypeEnum.Long:
                    return Convert.ToInt64(result);
                case TagDataTypeEnum.Float:
                    return Convert.ToSingle(result);
                case TagDataTypeEnum.Double:
                    return Convert.ToDouble(result);
            }
            return ValueLast;
        }

        private object? GetCalculatedValue(CalculationTypeEnum calculationType, Queue<object?> queue)
        {
            if (!queue.Any(q => q != null))
            {
                return null;
            }

            switch (calculationType)
            {
                case CalculationTypeEnum.Max:
                    return queue.Max();
                case CalculationTypeEnum.Avg:
                    switch (DataType)
                    {
                        case TagDataTypeEnum.Integer:
                        case TagDataTypeEnum.Float:
                        case TagDataTypeEnum.Double:
                            return queue.Where(q => q != null).Select(x => Convert.ToDouble(x)).Average();
                        case TagDataTypeEnum.Long:
                            return queue.Where(q => q != null).Select(x => Convert.ToInt64(x)).Average();
                    }
                    break;
                case CalculationTypeEnum.Min:
                    return queue.Min();
            }
            return ValueLast;
        }

        public void ValueBufferEnqueue(object? value)
        {
            ValueLast = value;
            ValueBuffer.Enqueue(ValueLast);
            if(ValueBuffer.Count > WindowSize)
            {
                ValueBuffer.Dequeue();
            }
        }

        private enum CalculationTypeEnum
        {
            Last = 1,
            Max = 2,
            Avg = 3,
            Min = 4
        }
    }
}
