using AssetMonitorDataAccess.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace AssetMonitorService.Monitor.Model
{
    public class TagHistoricalValue
    {
        public readonly TagDataTypeEnum DataType;
        public readonly HistoricalTypeEnum HistoricalType;
        public readonly int WindowSize;

        public TagHistoricalValue(TagDataTypeEnum dataType, HistoricalTypeEnum historicalType, int windowSize)
        {
            this.DataType = dataType;
            this.HistoricalType = historicalType;
            this.WindowSize = windowSize;
            ValueBuffer = new Queue<object>();
        }

        public Queue<object> ValueBuffer { get; private set; }
        public object LastValue { get; private set; }

        public object Value { 
            get
            {
                if(DataType == TagDataTypeEnum.Integer ||
                    DataType == TagDataTypeEnum.Long ||
                    DataType == TagDataTypeEnum.Float ||
                    DataType == TagDataTypeEnum.Double)
                {
                    if(HistoricalType == HistoricalTypeEnum.Last)
                    {
                        return LastValue;
                    }
                    var tempList = ValueBuffer.Cast<double>().ToList();
                    double result = 0.0;
                    switch (HistoricalType)
                    {
                        case HistoricalTypeEnum.Maximum:
                            result = tempList.Max();
                            break; 
                        case HistoricalTypeEnum.Average:
                            result = tempList.Average();
                            break;
                        case HistoricalTypeEnum.Minimum:
                            result = tempList.Min();
                            break;
                    }
                    switch (DataType)
                    {
                        case TagDataTypeEnum.Integer:
                            return (int)result;
                        case TagDataTypeEnum.Long:
                            return (long)result;
                        case TagDataTypeEnum.Float:
                            return (float)result;
                        case TagDataTypeEnum.Double:
                            return (double)result;
                    }
                }
                return LastValue;
            }
        }

        public void ValueBufferEnqueue(object value)
        {
            LastValue = value;
            ValueBuffer.Enqueue(LastValue);
            if(ValueBuffer.Count > WindowSize)
            {
                ValueBuffer.Dequeue();
            }
        }
    }
}
