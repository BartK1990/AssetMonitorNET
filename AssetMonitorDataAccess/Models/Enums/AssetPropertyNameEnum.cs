﻿using System.Collections.Generic;

namespace AssetMonitorDataAccess.Models.Enums
{
    public enum AssetPropertyNameEnum
    {
        AgentTcpPort = 1,
        SnmpUdpPort = 2,
        SnmpTimeout = 3,
        SnmpRetries = 4,
        SnmpCommunity = 5,
        SnmpVersion = 6,
        EmailNotificationsEnable = 7
    }

    public static class AssetPropertyNameDictionary
    {
        public static readonly Dictionary<AssetPropertyNameEnum, AssetProperty> Dict = new Dictionary<AssetPropertyNameEnum, AssetProperty>();

        static AssetPropertyNameDictionary()
        {
            AssetPropertyNameEnum id;
            id = AssetPropertyNameEnum.AgentTcpPort; Dict.Add(id, new AssetProperty() { Id = (int)id, Name = id.ToString(), Description = "Agent TCP Port", ValueDataTypeId = (int)AssetPropertyDataTypeEnum.Integer });
            id = AssetPropertyNameEnum.SnmpUdpPort; Dict.Add(id, new AssetProperty() { Id = (int)id, Name = id.ToString(), Description = "SNMP UDP Port", ValueDataTypeId = (int)AssetPropertyDataTypeEnum.Integer });
            id = AssetPropertyNameEnum.SnmpTimeout; Dict.Add(id, new AssetProperty() { Id = (int)id, Name = id.ToString(), Description = "SNMP timeout", ValueDataTypeId = (int)AssetPropertyDataTypeEnum.Integer });
            id = AssetPropertyNameEnum.SnmpRetries; Dict.Add(id, new AssetProperty() { Id = (int)id, Name = id.ToString(), Description = "SNMP number of retries", ValueDataTypeId = (int)AssetPropertyDataTypeEnum.Integer });
            id = AssetPropertyNameEnum.SnmpCommunity; Dict.Add(id, new AssetProperty() { Id = (int)id, Name = id.ToString(), Description = "SNMP Community String", ValueDataTypeId = (int)AssetPropertyDataTypeEnum.String });
            id = AssetPropertyNameEnum.SnmpVersion; Dict.Add(id, new AssetProperty() { Id = (int)id, Name = id.ToString(), Description = "SNMP Version (1 or 2)", ValueDataTypeId = (int)AssetPropertyDataTypeEnum.String });
            id = AssetPropertyNameEnum.EmailNotificationsEnable; Dict.Add(id, new AssetProperty() { Id = (int)id, Name = id.ToString(), Description = "Enable or disable email notifications", ValueDataTypeId = (int)AssetPropertyDataTypeEnum.Boolean });
        }
    }
}
