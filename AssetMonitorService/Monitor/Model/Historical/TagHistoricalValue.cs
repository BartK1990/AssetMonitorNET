using AssetMonitorDataAccess.Models.Enums;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

namespace AssetMonitorService.Monitor.Model.Historical
{
    public class TagHistoricalValue
    {
        public readonly TagDataTypeEnum DataType;
        public readonly int WindowSize;

        public TagHistoricalValue(TagDataTypeEnum dataType, int windowSize)
        {
            this.DataType = dataType;
            this.WindowSize = windowSize;
            ValueBuffer = new Queue<object>();
        }

        public Queue<object> ValueBuffer { get; private set; }

        private object _valueLast;
        public object ValueLast 
        { 
            get 
            {
                if (_valueLast == null)
                {
                    switch (DataType)
                    {
                        case TagDataTypeEnum.Boolean:
                            return false;
                        case TagDataTypeEnum.Integer:
                            return 0;
                        case TagDataTypeEnum.Float:
                            return 0.0F;
                        case TagDataTypeEnum.Double:
                            return 0.0D;
                        case TagDataTypeEnum.String:
                            return string.Empty;
                        case TagDataTypeEnum.Long:
                            return 0L;
                    }
                }
                return _valueLast;
            }
            private set 
            {
                _valueLast = value;
            } 
        }

        public object ValueMax 
        {
            get
            {
                return GetSpecificType(CalculationTypeEnum.Max);
            }
        }
        public object ValueAvg
        {
            get
            {
                return GetSpecificType(CalculationTypeEnum.Avg);
            }
        }
        public object ValueMin
        {
            get
            {
                return GetSpecificType(CalculationTypeEnum.Min);
            }
        }

        private object GetSpecificType(CalculationTypeEnum calculationType)
        {
            if ((DataType != TagDataTypeEnum.Integer &&
                DataType != TagDataTypeEnum.Long &&
                DataType != TagDataTypeEnum.Float &&
                DataType != TagDataTypeEnum.Double)
                || !ValueBuffer.Any())
            {
                return ValueLast;
            }

            object result = GetCalculatedValue(calculationType, ValueBuffer);

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

        private object GetCalculatedValue(CalculationTypeEnum calculationType, Queue<object> queue)
        {
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
                            return queue.Select(x => Convert.ToDouble(x)).Average();
                        case TagDataTypeEnum.Long:
                            return queue.Select(x => Convert.ToInt64(x)).Average();
                    }
                    break;
                case CalculationTypeEnum.Min:
                    return queue.Min();
            }
            return ValueLast;
        }

        public void ValueBufferEnqueue(object value)
        {
            if (value == null)
            {
                return;
            }

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
