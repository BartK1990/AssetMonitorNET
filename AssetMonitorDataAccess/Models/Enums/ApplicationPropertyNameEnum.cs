using System.Collections.Generic;

namespace AssetMonitorDataAccess.Models.Enums
{
    public enum ApplicationPropertyNameEnum
    {
        AssetsTimedIcmpDataScanTime = 1,
        AssetsTimedPerformanceDataScanTime = 2,
        AssetsTimedSnmpDataScanTime = 3,
        AssetsNotificationTimedScanTime = 4,
        AssetsAlarmTimedScanTime = 5,
        AssetsHistoryTimedScanTime = 6,
        FrontEndScanTime = 7

    }

    public static class ApplicationPropertyNameDictionary
    {
        public static readonly Dictionary<ApplicationPropertyNameEnum, ApplicationProperty> Dict = new Dictionary<ApplicationPropertyNameEnum, ApplicationProperty>();

        static ApplicationPropertyNameDictionary()
        {
            ApplicationPropertyNameEnum id;
            id = ApplicationPropertyNameEnum.AssetsTimedIcmpDataScanTime; Dict.Add(id, new ApplicationProperty() { Id = (int)id, Name = id.ToString(), Description = "Scan time of ICMP data Service", ValueDataTypeId = (int)ApplicationPropertyDataTypeEnum.Integer });
            id = ApplicationPropertyNameEnum.AssetsTimedPerformanceDataScanTime; Dict.Add(id, new ApplicationProperty() { Id = (int)id, Name = id.ToString(), Description = "Scan time of Agent/Performance data Service", ValueDataTypeId = (int)ApplicationPropertyDataTypeEnum.Integer });
            id = ApplicationPropertyNameEnum.AssetsTimedSnmpDataScanTime; Dict.Add(id, new ApplicationProperty() { Id = (int)id, Name = id.ToString(), Description = "Scan time of SNMP data Service", ValueDataTypeId = (int)ApplicationPropertyDataTypeEnum.Integer });
            id = ApplicationPropertyNameEnum.AssetsNotificationTimedScanTime; Dict.Add(id, new ApplicationProperty() { Id = (int)id, Name = id.ToString(), Description = "Scan time of Notifications Service", ValueDataTypeId = (int)ApplicationPropertyDataTypeEnum.Integer });
            id = ApplicationPropertyNameEnum.AssetsAlarmTimedScanTime; Dict.Add(id, new ApplicationProperty() { Id = (int)id, Name = id.ToString(), Description = "Scan time of Alarms Service", ValueDataTypeId = (int)ApplicationPropertyDataTypeEnum.Integer });
            id = ApplicationPropertyNameEnum.AssetsHistoryTimedScanTime; Dict.Add(id, new ApplicationProperty() { Id = (int)id, Name = id.ToString(), Description = "Scan time of Historical data Service", ValueDataTypeId = (int)ApplicationPropertyDataTypeEnum.Integer });
            id = ApplicationPropertyNameEnum.FrontEndScanTime; Dict.Add(id, new ApplicationProperty() { Id = (int)id, Name = id.ToString(), Description = "Scan time for reading data from main Windows Service", ValueDataTypeId = (int)ApplicationPropertyDataTypeEnum.Integer });
        }
    }
}
