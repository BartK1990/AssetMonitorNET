using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace AssetMonitorDataAccess.DataAccess
{
    public class AssetMonitorContext : DbContext
    {
        public AssetMonitorContext(DbContextOptions<AssetMonitorContext> options) : base(options)
        {
        }

        public DbSet<Asset> Asset { get; set; }
        public DbSet<AssetProperty> AssetProperty { get; set; }
        public DbSet<AssetPropertyDataType> AssetPropertyDataType { get; set; }
        public DbSet<AssetPropertyValue> AssetPropertyValue { get; set; }
        public DbSet<AgentDataType> AgentDataType { get; set; }
        public DbSet<AgentTag> AgentTag { get; set; }
        public DbSet<AlarmTagConfig> AlarmTagConfig { get; set; }
        public DbSet<AlarmType> AlarmType { get; set; }
        public DbSet<HistoricalTagConfig> HistoricalTagConfig { get; set; }
        public DbSet<HistoricalType> HistoricalType { get; set; }
        public DbSet<SnmpAssetValue> SnmpAssetValue { get; set; }
        public DbSet<SnmpCommunicationType> SnmpCommunicationType { get; set; }
        public DbSet<SnmpOperation> SnmpOperation { get; set; }
        public DbSet<SnmpTag> SnmpTag { get; set; }
        public DbSet<SnmpVersion> SnmpVersion { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<TagCommunicationRel> TagCommunicationRel { get; set; }
        public DbSet<TagDataType> TagDataType { get; set; }
        public DbSet<TagSet> TagSet { get; set; }
        public DbSet<UserEmailAddress> UserEmailAddress { get; set; }
        public DbSet<UserEmailAddressSet> UserEmailAddressSet { get; set; }
        public DbSet<UserEmailAssetRel> UserEmailAssetRel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tag>().HasIndex(c => new { c.TagSetId, c.Tagname }).IsUnique();
            modelBuilder.Entity<Tag>().HasIndex(c => new { c.TagCommunicationRelId }).IsUnique();
            modelBuilder.Entity<Tag>().Property(p => p.ScaleFactor).HasDefaultValue(1.0);
            modelBuilder.Entity<Tag>().Property(p => p.ScaleOffset).HasDefaultValue(0.0);

            modelBuilder.Entity<TagCommunicationRel>(entity =>
                entity.HasCheckConstraint("CK_AlarmTagConfig_PingOrAgentOrSnmp",
                $"([{nameof(Models.TagCommunicationRel.IcmpTag)}] = 1 AND [{nameof(Models.TagCommunicationRel.AgentTagId)}] IS NULL AND [{nameof(Models.TagCommunicationRel.SnmpTagId)}] IS NULL) OR " +
                $"([{nameof(Models.TagCommunicationRel.IcmpTag)}] = 0 AND [{nameof(Models.TagCommunicationRel.AgentTagId)}] IS NOT NULL AND [{nameof(Models.TagCommunicationRel.SnmpTagId)}] IS NULL) OR " +
                $"([{nameof(Models.TagCommunicationRel.IcmpTag)}] = 0 AND [{nameof(Models.TagCommunicationRel.AgentTagId)}] IS NULL AND [{nameof(Models.TagCommunicationRel.SnmpTagId)}] IS NOT NULL)"));

            modelBuilder.Entity<HistoricalTagConfig>()
                .HasIndex(c => new { c.TagId, c.HistorizationTypeId }).IsUnique();

            modelBuilder.Entity<AlarmTagConfig>()
                .HasIndex(c => new { c.TagId, c.AlarmTypeId }).IsUnique();

            modelBuilder.Entity<AgentTag>(entity =>
                entity.HasCheckConstraint("CK_AgentTag_NotNullTagInfo",
                $"[{nameof(Models.AgentTag.PerformanceCounter)}] IS NOT NULL OR [{nameof(Models.AgentTag.WmiManagementObject)}] IS NOT NULL OR [{nameof(Models.AgentTag.ServiceName)}] IS NOT NULL"));

            #region Enums
            Array enums = Enum.GetValues(typeof(SnmpOperationEnum));
            foreach (var item in enums)
            {
                modelBuilder.Entity<SnmpOperation>().HasData(new SnmpOperation()
                {
                    Id = (int)item,
                    Operation = Enum.GetName(typeof(SnmpOperationEnum), item)
                });
            }

            enums = Enum.GetValues(typeof(TagDataTypeEnum));
            foreach (var item in enums)
            {
                modelBuilder.Entity<TagDataType>().HasData(new TagDataType()
                {
                    Id = (int)item,
                    DataType = Enum.GetName(typeof(TagDataTypeEnum), item)
                });
            }

            enums = Enum.GetValues(typeof(AgentDataTypeEnum));
            foreach (var item in enums)
            {
                modelBuilder.Entity<AgentDataType>().HasData(new AgentDataType()
                {
                    Id = (int)item,
                    DataType = Enum.GetName(typeof(AgentDataTypeEnum), item)
                });
            }

            enums = Enum.GetValues(typeof(AssetPropertyDataTypeEnum));
            foreach (var item in enums)
            {
                modelBuilder.Entity<AssetPropertyDataType>().HasData(new AssetPropertyDataType()
                {
                    Id = (int)item,
                    DataType = Enum.GetName(typeof(AssetPropertyDataTypeEnum), item)
                });
            }

            enums = Enum.GetValues(typeof(SnmpVersionEnum));
            foreach (var item in enums)
            {
                modelBuilder.Entity<SnmpVersion>().HasData(new SnmpVersion()
                {
                    Id = (int)item,
                    Version = Enum.GetName(typeof(SnmpVersionEnum), item)
                });
            }

            enums = Enum.GetValues(typeof(HistoricalTypeEnum));
            foreach (var item in enums)
            {
                modelBuilder.Entity<HistoricalType>().HasData(new HistoricalType()
                {
                    Id = (int)item,
                    Type = Enum.GetName(typeof(HistoricalTypeEnum), item)
                });
            }

            enums = Enum.GetValues(typeof(AlarmTypeEnum));
            foreach (var item in enums)
            {
                modelBuilder.Entity<AlarmType>().HasData(new AlarmType()
                {
                    Id = (int)item,
                    Type = Enum.GetName(typeof(AlarmTypeEnum), item)
                });
            }

            enums = Enum.GetValues(typeof(SnmpCommunicationTypeEnum));
            foreach (var item in enums)
            {
                modelBuilder.Entity<SnmpCommunicationType>().HasData(new AlarmType()
                {
                    Id = (int)item,
                    Type = Enum.GetName(typeof(SnmpCommunicationTypeEnum), item)
                });
            }

            enums = Enum.GetValues(typeof(AssetPropertyNameEnum));
            foreach (var item in enums)
            {
                modelBuilder.Entity<AssetProperty>().HasData(AssetPropertyNameDictionary.Dict[(AssetPropertyNameEnum)item]);
            }
            #endregion

            modelBuilder.Entity<AgentTag>().HasData(
                new AgentTag() { Id = 1, AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"Processor;% Processor Time;_Total" },
                new AgentTag() { Id = 2, AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"Memory;Available MBytes" },
                new AgentTag() { Id = 3, AgentDataTypeId = (int)AgentDataTypeEnum.WMI, WmiManagementObject = @"TotalPhysicalMemory" },
                new AgentTag() { Id = 4, AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"PhysicalDisk;% Idle Time;_Total" },
                new AgentTag() { Id = 5, AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"PhysicalDisk;% Disk Time;_Total" },
                new AgentTag() { Id = 6, AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"LogicalDisk;% Free Space;_Total" }
                );

            modelBuilder.Entity<SnmpTag>().HasData(
                // TagSet Id = 1
                new SnmpTag() { Id = 1, OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.5.0" },
                new SnmpTag() { Id = 2, OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.1.0" },
                new SnmpTag() { Id = 3, OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.2.0" },
                new SnmpTag() { Id = 4, OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.3.0" },
                new SnmpTag() { Id = 5, OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.4.0" },

                // TagSet Id = 2
                new SnmpTag() { Id = 6, OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.5.0" },
                new SnmpTag() { Id = 7, OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.1.0" },
                new SnmpTag() { Id = 8, OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.2.0" },
                new SnmpTag() { Id = 9, OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.3.0" },
                new SnmpTag() { Id = 10, OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.4.0" }
                );

            modelBuilder.Entity<TagCommunicationRel>().HasData(
                // TagSet Id = 1
                new TagCommunicationRel() { Id = 1, IcmpTag = true },
                new TagCommunicationRel() { Id = 2, IcmpTag = true },
                new TagCommunicationRel() { Id = 3, IcmpTag = false, AgentTagId = 1 },
                new TagCommunicationRel() { Id = 4, IcmpTag = false, AgentTagId = 2 },
                new TagCommunicationRel() { Id = 5, IcmpTag = false, AgentTagId = 3 },
                new TagCommunicationRel() { Id = 6, IcmpTag = false, AgentTagId = 4 },
                new TagCommunicationRel() { Id = 7, IcmpTag = false, AgentTagId = 5 },
                new TagCommunicationRel() { Id = 8, IcmpTag = false, AgentTagId = 6 },
                new TagCommunicationRel() { Id = 9, IcmpTag = false, SnmpTagId = 1 },
                new TagCommunicationRel() { Id = 10, IcmpTag = false, SnmpTagId = 2 },
                new TagCommunicationRel() { Id = 11, IcmpTag = false, SnmpTagId = 3 },
                new TagCommunicationRel() { Id = 12, IcmpTag = false, SnmpTagId = 4 },
                new TagCommunicationRel() { Id = 13, IcmpTag = false, SnmpTagId = 5 },

                // TagSet Id = 2
                new TagCommunicationRel() { Id = 14, IcmpTag = true },
                new TagCommunicationRel() { Id = 15, IcmpTag = true },
                new TagCommunicationRel() { Id = 16, IcmpTag = false, SnmpTagId = 6 },
                new TagCommunicationRel() { Id = 17, IcmpTag = false, SnmpTagId = 7 },
                new TagCommunicationRel() { Id = 18, IcmpTag = false, SnmpTagId = 8 },
                new TagCommunicationRel() { Id = 19, IcmpTag = false, SnmpTagId = 9 },
                new TagCommunicationRel() { Id = 20, IcmpTag = false, SnmpTagId = 10 }
                );

            modelBuilder.Entity<TagSet>().HasData(
                new TagSet() { Id = 1, Name = "Windows Agent and SNMP Default" },
                new TagSet() { Id = 2, Name = "SNMP Default" }
                );

            modelBuilder.Entity<Tag>().HasData(
                new Tag() { Id = 1, TagCommunicationRelId = 1, TagSetId = 1, Tagname = "ICMP.PingState", ValueDataTypeId = (int)TagDataTypeEnum.Boolean },
                new Tag() { Id = 2, TagCommunicationRelId = 2, TagSetId = 1, Tagname = "ICMP.PingResponseTime", ValueDataTypeId = (int)TagDataTypeEnum.Long },
                new Tag() { Id = 3, TagCommunicationRelId = 3, TagSetId = 1, Tagname = "Agent.CpuUsage", ValueDataTypeId = (int)TagDataTypeEnum.Float },
                new Tag() { Id = 4, TagCommunicationRelId = 4, TagSetId = 1, Tagname = "Agent.MemoryAvailable", ValueDataTypeId = (int)TagDataTypeEnum.Float },
                new Tag() { Id = 5, TagCommunicationRelId = 5, TagSetId = 1, Tagname = "Agent.MemoryTotal", ValueDataTypeId = (int)TagDataTypeEnum.Double },
                new Tag() { Id = 6, TagCommunicationRelId = 6, TagSetId = 1, Tagname = "Agent.PhysicalDiskIdleTime", ValueDataTypeId = (int)TagDataTypeEnum.Float },
                new Tag() { Id = 7, TagCommunicationRelId = 7, TagSetId = 1, Tagname = "Agent.PhysicalDiskWorkTime", ValueDataTypeId = (int)TagDataTypeEnum.Float },
                new Tag() { Id = 8, TagCommunicationRelId = 8, TagSetId = 1, Tagname = "Agent.LogicalDiskFreeSpace", ValueDataTypeId = (int)TagDataTypeEnum.Float },
                new Tag() { Id = 9, TagCommunicationRelId = 9, TagSetId = 1, Tagname = "SNMP.sysName", ValueDataTypeId = (int)TagDataTypeEnum.String },
                new Tag() { Id = 10, TagCommunicationRelId = 10, TagSetId = 1, Tagname = "SNMP.sysDescr", ValueDataTypeId = (int)TagDataTypeEnum.String },
                new Tag() { Id = 11, TagCommunicationRelId = 11, TagSetId = 1, Tagname = "SNMP.sysObjectID", ValueDataTypeId = (int)TagDataTypeEnum.String },
                new Tag() { Id = 12, TagCommunicationRelId = 12, TagSetId = 1, Tagname = "SNMP.sysUpTime", ValueDataTypeId = (int)TagDataTypeEnum.String },
                new Tag() { Id = 13, TagCommunicationRelId = 13, TagSetId = 1, Tagname = "SNMP.sysContact", ValueDataTypeId = (int)TagDataTypeEnum.String },

                new Tag() { Id = 14, TagCommunicationRelId = 14, TagSetId = 2, Tagname = "ICMP.PingState", ValueDataTypeId = (int)TagDataTypeEnum.Boolean },
                new Tag() { Id = 15, TagCommunicationRelId = 15, TagSetId = 2, Tagname = "ICMP.PingResponseTime", ValueDataTypeId = (int)TagDataTypeEnum.Long },
                new Tag() { Id = 16, TagCommunicationRelId = 16, TagSetId = 2, Tagname = "SNMP.sysName", ValueDataTypeId = (int)TagDataTypeEnum.String },
                new Tag() { Id = 17, TagCommunicationRelId = 17, TagSetId = 2, Tagname = "SNMP.sysDescr", ValueDataTypeId = (int)TagDataTypeEnum.String },
                new Tag() { Id = 18, TagCommunicationRelId = 18, TagSetId = 2, Tagname = "SNMP.sysObjectID", ValueDataTypeId = (int)TagDataTypeEnum.String },
                new Tag() { Id = 19, TagCommunicationRelId = 19, TagSetId = 2, Tagname = "SNMP.sysUpTime", ValueDataTypeId = (int)TagDataTypeEnum.String },
                new Tag() { Id = 20, TagCommunicationRelId = 20, TagSetId = 2, Tagname = "SNMP.sysContact", ValueDataTypeId = (int)TagDataTypeEnum.String }
               );

            modelBuilder.Entity<HistoricalTagConfig>().HasData(
                // TagSet Id = 1
                new HistoricalTagConfig() { Id = 1, TagId = 1, HistorizationTypeId = (int)HistoricalTypeEnum.Last },
                new HistoricalTagConfig() { Id = 2, TagId = 2, HistorizationTypeId = (int)HistoricalTypeEnum.Average },
                new HistoricalTagConfig() { Id = 3, TagId = 3, HistorizationTypeId = (int)HistoricalTypeEnum.Average },
                new HistoricalTagConfig() { Id = 4, TagId = 3, HistorizationTypeId = (int)HistoricalTypeEnum.Maximum },
                new HistoricalTagConfig() { Id = 5, TagId = 4, HistorizationTypeId = (int)HistoricalTypeEnum.Average },
                new HistoricalTagConfig() { Id = 6, TagId = 5, HistorizationTypeId = (int)HistoricalTypeEnum.Average },
                new HistoricalTagConfig() { Id = 7, TagId = 6, HistorizationTypeId = (int)HistoricalTypeEnum.Average },
                new HistoricalTagConfig() { Id = 8, TagId = 7, HistorizationTypeId = (int)HistoricalTypeEnum.Average },
                new HistoricalTagConfig() { Id = 9, TagId = 8, HistorizationTypeId = (int)HistoricalTypeEnum.Average },
                new HistoricalTagConfig() { Id = 10, TagId = 12, HistorizationTypeId = (int)HistoricalTypeEnum.Average },

                // TagSet Id = 2
                new HistoricalTagConfig() { Id = 11, TagId = 14, HistorizationTypeId = (int)HistoricalTypeEnum.Last },
                new HistoricalTagConfig() { Id = 12, TagId = 15, HistorizationTypeId = (int)HistoricalTypeEnum.Average },
                new HistoricalTagConfig() { Id = 13, TagId = 19, HistorizationTypeId = (int)HistoricalTypeEnum.Last }
                );

            modelBuilder.Entity<AlarmTagConfig>().HasData(
                // TagSet Id = 1
                new AlarmTagConfig() { Id = 1, TagId = 1, AlarmTypeId = (int)AlarmTypeEnum.Equal, ActivationTime = 30, Value = "False", Description = "No ping!" },
                new AlarmTagConfig() { Id = 2, TagId = 3, AlarmTypeId = (int)AlarmTypeEnum.GreaterOrEqual, ActivationTime = 30, Value = "50", Description = "CPU usage is to high!" },

                // TagSet Id = 2
                new AlarmTagConfig() { Id = 3, TagId = 14, AlarmTypeId = (int)AlarmTypeEnum.Equal, ActivationTime = 30, Value = "False", Description = "No ping!" }
                );

            modelBuilder.Entity<Asset>()
                .HasData(new Asset()
                {
                    Id = 1,
                    Name = "AssetMonitorNET Server",
                    IpAddress = "127.0.0.1",
                    TagSetId = 1
                });

            modelBuilder.Entity<AssetPropertyValue>().HasData(
                new AssetPropertyValue() { Id = -1, AssetPropertyId = (int)AssetPropertyNameEnum.SnmpUdpPort, AssetId = 1, Value = "161" },
                new AssetPropertyValue() { Id = -2, AssetPropertyId = (int)AssetPropertyNameEnum.SnmpTimeout, AssetId = 1, Value = "3000" },
                new AssetPropertyValue() { Id = -3, AssetPropertyId = (int)AssetPropertyNameEnum.SnmpRetries, AssetId = 1, Value = "1" },
                new AssetPropertyValue() { Id = -4, AssetPropertyId = (int)AssetPropertyNameEnum.SnmpCommunity, AssetId = 1, Value = "public" },
                new AssetPropertyValue() { Id = -5, AssetPropertyId = (int)AssetPropertyNameEnum.SnmpVersion, AssetId = 1, Value = "2" }
                );
        }
    }
}
