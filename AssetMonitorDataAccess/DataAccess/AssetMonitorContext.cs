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
        public DbSet<AgentTagSet> AgentTagSet { get; set; }
        public DbSet<AlarmTagConfig> AlarmTagConfig { get; set; }
        public DbSet<AlarmType> AlarmType { get; set; }
        public DbSet<HistoricalTagConfig> HistoricalTagConfig { get; set; }
        public DbSet<HistoricalType> HistoricalType { get; set; }
        public DbSet<HttpNodeRedTag> HttpNodeRedTag { get; set; }
        public DbSet<HttpNodeRedTagSet> HttpNodeRedTagSet { get; set; }
        public DbSet<IcmpTag> IcmpTag { get; set; }
        public DbSet<IcmpTagSet> IcmpTagSet { get; set; }
        public DbSet<SnmpAssetValue> SnmpAssetValue { get; set; }
        public DbSet<SnmpOperation> SnmpOperation { get; set; }
        public DbSet<SnmpTag> SnmpTag { get; set; }
        public DbSet<SnmpTagSet> SnmpTagSet { get; set; }
        public DbSet<SnmpVersion> SnmpVersion { get; set; }
        public DbSet<TagDataType> TagDataType { get; set; }
        public DbSet<UserEmailAddress> UserEmailAddress { get; set; }
        public DbSet<UserEmailAssetRel> UserEmailAssetRel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AgentTag>()
                .HasIndex(c => new { c.AgentTagSetId, c.Tagname }).IsUnique();
            modelBuilder.Entity<AgentTag>().Property(p => p.ScaleFactor).HasDefaultValue(1.0);
            modelBuilder.Entity<AgentTag>().Property(p => p.ScaleOffset).HasDefaultValue(0.0);

            modelBuilder.Entity<SnmpTag>()
                .HasIndex(c => new { c.SnmpTagSetId, c.Tagname }).IsUnique();
            modelBuilder.Entity<SnmpTag>().Property(p => p.ScaleFactor).HasDefaultValue(1.0);
            modelBuilder.Entity<SnmpTag>().Property(p => p.ScaleOffset).HasDefaultValue(0.0);

            modelBuilder.Entity<HttpNodeRedTag>()
                .HasIndex(c => new { c.Id, c.Tagname }).IsUnique();
            modelBuilder.Entity<HttpNodeRedTag>().Property(p => p.ScaleFactor).HasDefaultValue(1.0);
            modelBuilder.Entity<HttpNodeRedTag>().Property(p => p.ScaleOffset).HasDefaultValue(0.0);

            modelBuilder.Entity<HistoricalTagConfig>()
                .HasIndex(c => new { c.AgentTagId, c.SnmpTagId, c.HistorizationTypeId }).IsUnique();

            modelBuilder.Entity<AgentTag>(entity =>
                entity.HasCheckConstraint("CK_AgentTag_NotNullTagInfo",
                $"[{nameof(Models.AgentTag.PerformanceCounter)}] IS NOT NULL OR [{nameof(Models.AgentTag.WmiManagementObject)}] IS NOT NULL OR [{nameof(Models.AgentTag.ServiceName)}] IS NOT NULL"));

            modelBuilder.Entity<HistoricalTagConfig>(entity =>
                entity.HasCheckConstraint("CK_HistorizationTagConfig_AgentOrSnmp",
                $"([{nameof(Models.HistoricalTagConfig.AgentTagId)}] IS NOT NULL AND [{nameof(Models.HistoricalTagConfig.SnmpTagId)}] IS NULL) OR " +
                $"([{nameof(Models.HistoricalTagConfig.AgentTagId)}] IS NULL AND [{nameof(Models.HistoricalTagConfig.SnmpTagId)}] IS NOT NULL)"));

            modelBuilder.Entity<AlarmTagConfig>(entity =>
                entity.HasCheckConstraint("CK_AlarmTagConfig_PingOrAgentOrSnmp",
                $"([{nameof(Models.AlarmTagConfig.IcmpTagId)}] IS NOT NULL AND [{nameof(Models.AlarmTagConfig.AgentTagId)}] IS NULL AND [{nameof(Models.AlarmTagConfig.SnmpTagId)}] IS NULL) OR " +
                $"([{nameof(Models.AlarmTagConfig.IcmpTagId)}] IS NULL AND [{nameof(Models.AlarmTagConfig.AgentTagId)}] IS NOT NULL AND [{nameof(Models.AlarmTagConfig.SnmpTagId)}] IS NULL) OR " +
                $"([{nameof(Models.AlarmTagConfig.IcmpTagId)}] IS NULL AND [{nameof(Models.AlarmTagConfig.AgentTagId)}] IS NULL AND [{nameof(Models.AlarmTagConfig.SnmpTagId)}] IS NOT NULL)"));

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

            enums = Enum.GetValues(typeof(AssetPropertyNameEnum));
            foreach (var item in enums)
            {
                modelBuilder.Entity<AssetProperty>().HasData(AssetPropertyNameDictionary.Dict[(AssetPropertyNameEnum)item]);
            }
            #endregion

            modelBuilder.Entity<IcmpTagSet>()
                .HasData(new IcmpTagSet()
                {
                    Id = 1,
                    Name = "Default"
                });

            modelBuilder.Entity<AgentTagSet>()
                .HasData(new AgentTagSet()
                {
                    Id = 1,
                    Name = "Default"
                });

            modelBuilder.Entity<SnmpTagSet>()
                .HasData(new SnmpTagSet()
                {
                    Id = 1,
                    Name = "Default",
                    VersionId = (int)SnmpVersionEnum.V2c
                });

            modelBuilder.Entity<IcmpTag>().HasData(
               new IcmpTag() { Id = 1, Tagname = "PingState", ValueDataTypeId = (int)TagDataTypeEnum.Boolean, IcmpTagSetId = 1 },
               new IcmpTag() { Id = 2, Tagname = "PingResponseTime", ValueDataTypeId = (int)TagDataTypeEnum.Long, IcmpTagSetId = 1 }
               );

            modelBuilder.Entity<AgentTag>().HasData(
                new AgentTag() {Id = 1, Tagname = "CpuUsage", AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"Processor;% Processor Time;_Total", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 },
                new AgentTag() {Id = 2, Tagname = "MemoryAvailable", AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"Memory;Available MBytes", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 },
                new AgentTag() {Id = 3, Tagname = "MemoryTotal", AgentDataTypeId = (int)AgentDataTypeEnum.WMI, WmiManagementObject = @"TotalPhysicalMemory", ValueDataTypeId = (int)TagDataTypeEnum.Double, AgentTagSetId = 1 },
                new AgentTag() {Id = 4, Tagname = "PhysicalDiskIdleTime", AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"PhysicalDisk;% Idle Time;_Total", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 },
                new AgentTag() {Id = 5, Tagname = "PhysicalDiskWorkTime", AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"PhysicalDisk;% Disk Time;_Total", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 },
                new AgentTag() {Id = 6, Tagname = "LogicalDiskFreeSpace", AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"LogicalDisk;% Free Space;_Total", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 }
                );

            modelBuilder.Entity<SnmpTag>().HasData(
                new SnmpTag() { Id = 1, Tagname = "sysName", OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.5.0", ValueDataTypeId = (int)TagDataTypeEnum.String, SnmpTagSetId = 1 },
                new SnmpTag() { Id = 2, Tagname = "sysDescr", OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.1.0", ValueDataTypeId = (int)TagDataTypeEnum.String, SnmpTagSetId = 1 },
                new SnmpTag() { Id = 3, Tagname = "sysObjectID", OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.2.0", ValueDataTypeId = (int)TagDataTypeEnum.String, SnmpTagSetId = 1 },
                new SnmpTag() { Id = 4, Tagname = "sysUpTime", OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.3.0", ValueDataTypeId = (int)TagDataTypeEnum.String, SnmpTagSetId = 1 },
                new SnmpTag() { Id = 5, Tagname = "sysContact", OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.4.0", ValueDataTypeId = (int)TagDataTypeEnum.String, SnmpTagSetId = 1 }
                );

            modelBuilder.Entity<HistoricalTagConfig>().HasData(
                new HistoricalTagConfig() { Id = 1, AgentTagId = 1, HistorizationTypeId = (int)HistoricalTypeEnum.Average },
                new HistoricalTagConfig() { Id = 2, AgentTagId = 1, HistorizationTypeId = (int)HistoricalTypeEnum.Maximum },
                new HistoricalTagConfig() { Id = 3, AgentTagId = 2, HistorizationTypeId = (int)HistoricalTypeEnum.Average },
                new HistoricalTagConfig() { Id = 4, AgentTagId = 3, HistorizationTypeId = (int)HistoricalTypeEnum.Average },
                new HistoricalTagConfig() { Id = 5, AgentTagId = 4, HistorizationTypeId = (int)HistoricalTypeEnum.Average },
                new HistoricalTagConfig() { Id = 6, AgentTagId = 5, HistorizationTypeId = (int)HistoricalTypeEnum.Average },
                new HistoricalTagConfig() { Id = 7, AgentTagId = 6, HistorizationTypeId = (int)HistoricalTypeEnum.Average },

                new HistoricalTagConfig() { Id = 8, SnmpTagId = 4, HistorizationTypeId = (int)HistoricalTypeEnum.Last }
                );

            modelBuilder.Entity<AlarmTagConfig>().HasData(
                new AlarmTagConfig() { Id = 1, IcmpTagId = 1, AlarmTypeId = (int)AlarmTypeEnum.Equal, ActivationTime = 30, Value = "1", Description = "No ping!" },
                new AlarmTagConfig() { Id = 2, AgentTagId = 1, AlarmTypeId = (int)AlarmTypeEnum.GreaterOrEqual, ActivationTime = 30, Value = "50", Description = "CPU usage is to high!" }
                );

            modelBuilder.Entity<Asset>()
                .HasData(new Asset()
                {
                    Id = 1,
                    Name = "AssetMonitorNET Server",
                    IpAddress = "127.0.0.1",
                    IcmpTagSetId = 1,
                    AgentTagSetId = 1,
                    SnmpTagSetId = 1
                });

            modelBuilder.Entity<AssetPropertyValue>().HasData(
                new AssetPropertyValue() { Id = -1, AssetPropertyId = (int)AssetPropertyNameEnum.SnmpUdpPort, AssetId = 1, Value = "161" },
                new AssetPropertyValue() { Id = -2, AssetPropertyId = (int)AssetPropertyNameEnum.SnmpTimeout, AssetId = 1, Value = "3000" },
                new AssetPropertyValue() { Id = -3, AssetPropertyId = (int)AssetPropertyNameEnum.SnmpRetries, AssetId = 1, Value = "1" },
                new AssetPropertyValue() { Id = -4, AssetPropertyId = (int)AssetPropertyNameEnum.SnmpCommunity, AssetId = 1, Value = "public" }
                );
        }
    }
}
