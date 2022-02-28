using AssetMonitorDataAccess.Models.Enums;
using System;

namespace AssetMonitorService.Monitor.Model.Alarm
{
    public class TagAlarmValue : TagValueBase
    {
        public readonly TagDataTypeEnum DataType;
        public readonly AlarmTypeEnum AlarmType;

        public object AlarmValue { get; private set; }
        public int ActivationTime { get; private set; }
        public string Description { get; private set; }

        public TagAlarmValue(string tagname, TagDataTypeEnum dataType, 
            AlarmTypeEnum alarmType, object alarmValue, int activationTime, string description)
            : base(tagname: tagname)
        {
            this.DataType = dataType;
            this.AlarmType = alarmType;
            this.AlarmValue = alarmValue;
            this.ActivationTime = activationTime;
            this.Description = description;

            AlarmStateStartTime = DateTime.UtcNow;
        }

        public bool AlarmState { get; private set; }
        public DateTime AlarmSetTime { get; private set; }

        private bool LastAlarmState;
        private DateTime AlarmStateStartTime;

#nullable enable
        private object? _value;
        public object? Value 
        {
            get 
            {
                return _value;
            }
            set 
            {
                var alarmActive = GetAlarmState(value);

                if (alarmActive && (!LastAlarmState))
                {
                    AlarmStateStartTime = DateTime.UtcNow;
                }

                if (ActivationTime <= 0)
                {
                    AlarmState = alarmActive;
                    _value = value;
                    return;
                }

                if(alarmActive && LastAlarmState && (!AlarmState))
                {
                    if((DateTime.UtcNow - AlarmStateStartTime).TotalSeconds >= ActivationTime)
                    {
                        AlarmState = true;
                        AlarmSetTime = DateTime.UtcNow;
                    }
                }
                else
                {
                    if (!alarmActive)
                    {
                        AlarmState = false;
                    }
                }

                LastAlarmState = alarmActive;
                _value = value;
            }
        }

        private bool GetAlarmState(object? val)
        {
            if(val == null) // Reset alarm if null value passed
            {
                return false;
            }

            // Alarm can be checked only for equality/unequality if not numeric type
            if(DataType != TagDataTypeEnum.Integer &&
                DataType != TagDataTypeEnum.Float &&
                DataType != TagDataTypeEnum.Double &&
                DataType != TagDataTypeEnum.Long &&
                AlarmType != AlarmTypeEnum.Equal &&
                AlarmType != AlarmTypeEnum.NotEqual)
            {
                return false;
            }

            switch (AlarmType)
            {
                case AlarmTypeEnum.Equal:
                    if(DataType == TagDataTypeEnum.Boolean)
                    {
                        return AlarmValue.ToString().ToLower() == val.ToString().ToLower();
                    }
                    else
                    {
                        return AlarmValue.ToString() == val.ToString();
                    }
                case AlarmTypeEnum.NotEqual:
                    return AlarmValue.ToString() != val.ToString();
            }

            switch (DataType)
            {
                case TagDataTypeEnum.Integer:
                case TagDataTypeEnum.Float:
                case TagDataTypeEnum.Double:
                    var doubleValue = Convert.ToDouble(val);
                    var doubleAlarmValue = Convert.ToDouble(AlarmValue);
                    switch (AlarmType)
                    {
                        case AlarmTypeEnum.GreaterOrEqual:
                            return doubleValue >= doubleAlarmValue;
                        case AlarmTypeEnum.Greater:
                            return doubleValue > doubleAlarmValue;
                        case AlarmTypeEnum.LessOrEqual:
                            return doubleValue <= doubleAlarmValue;
                        case AlarmTypeEnum.Less:
                            return doubleValue < doubleAlarmValue;
                    }
                    break;
                case TagDataTypeEnum.Long:
                    var int64Value = Convert.ToInt64(val);
                    var int64AlarmValue = Convert.ToInt64(AlarmValue);
                    switch (AlarmType)
                    {
                        case AlarmTypeEnum.GreaterOrEqual:
                            return int64Value >= int64AlarmValue;
                        case AlarmTypeEnum.Greater:
                            return int64Value > int64AlarmValue;
                        case AlarmTypeEnum.LessOrEqual:
                            return int64Value <= int64AlarmValue;
                        case AlarmTypeEnum.Less:
                            return int64Value < int64AlarmValue;
                    }
                    break;
            }
            return false;
        }
#nullable disable

    }
}
