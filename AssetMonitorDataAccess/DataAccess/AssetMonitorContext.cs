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
        public DbSet<HttpNodeRedTag> HttpNodeRedTag { get; set; }
        public DbSet<HttpNodeRedTagSet> HttpNodeRedTagSet { get; set; }
        public DbSet<SnmpOperation> SnmpOperation { get; set; }
        public DbSet<SnmpTag> SnmpTag { get; set; }
        public DbSet<SnmpTagSet> SnmpTagSet { get; set; }
        public DbSet<SnmpVersion> SnmpVersion { get; set; }
        public DbSet<TagDataType> TagDataType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AgentTag>()
                .HasIndex(c => new { c.Id, c.Tagname }).IsUnique();
            modelBuilder.Entity<AgentTag>().Property(p => p.ScaleFactor).HasDefaultValue(1.0);
            modelBuilder.Entity<AgentTag>().Property(p => p.ScaleOffset).HasDefaultValue(0.0);
            modelBuilder.Entity<AgentTag>().Property(p => p.IsHistorized).HasDefaultValue(false);

            modelBuilder.Entity<SnmpTag>()
                .HasIndex(c => new { c.Id, c.Tagname }).IsUnique();
            modelBuilder.Entity<SnmpTag>().Property(p => p.ScaleFactor).HasDefaultValue(1.0);
            modelBuilder.Entity<SnmpTag>().Property(p => p.ScaleOffset).HasDefaultValue(0.0);
            modelBuilder.Entity<SnmpTag>().Property(p => p.IsHistorized).HasDefaultValue(false);

            modelBuilder.Entity<HttpNodeRedTag>()
                .HasIndex(c => new { c.Id, c.Tagname }).IsUnique();
            modelBuilder.Entity<HttpNodeRedTag>().Property(p => p.ScaleFactor).HasDefaultValue(1.0);
            modelBuilder.Entity<HttpNodeRedTag>().Property(p => p.ScaleOffset).HasDefaultValue(0.0);

            modelBuilder.Entity<AgentTag>(entity =>
                entity.HasCheckConstraint("CK_AgentTag_NotNullTagInfo",
                $"[{nameof(Models.AgentTag.PerformanceCounter)}] IS NOT NULL OR [{nameof(Models.AgentTag.WmiManagementObject)}] IS NOT NULL OR [{nameof(Models.AgentTag.ServiceName)}] IS NOT NULL"));

            #region Enums
            var ste = Enum.GetValues(typeof(SnmpOperationEnum));
            foreach (var item in ste)
            {
                modelBuilder.Entity<SnmpOperation>().HasData(new SnmpOperation()
                {
                    Id = (int)item,
                    Operation = Enum.GetName(typeof(SnmpOperationEnum), item)
                });
            }

            var tvte = Enum.GetValues(typeof(TagDataTypeEnum));
            foreach (var item in tvte)
            {
                modelBuilder.Entity<TagDataType>().HasData(new TagDataType()
                {
                    Id = (int)item,
                    DataType = Enum.GetName(typeof(TagDataTypeEnum), item)
                });
            }

            var adte = Enum.GetValues(typeof(AgentDataTypeEnum));
            foreach (var item in adte)
            {
                modelBuilder.Entity<AgentDataType>().HasData(new AgentDataType()
                {
                    Id = (int)item,
                    DataType = Enum.GetName(typeof(AgentDataTypeEnum), item)
                });
            }

            var apdte = Enum.GetValues(typeof(AssetPropertyDataTypeEnum));
            foreach (var item in apdte)
            {
                modelBuilder.Entity<AssetPropertyDataType>().HasData(new AssetPropertyDataType()
                {
                    Id = (int)item,
                    DataType = Enum.GetName(typeof(AssetPropertyDataTypeEnum), item)
                });
            }

            var sve = Enum.GetValues(typeof(SnmpVersionEnum));
            foreach (var item in sve)
            {
                modelBuilder.Entity<SnmpVersion>().HasData(new SnmpVersion()
                {
                    Id = (int)item,
                    Version = Enum.GetName(typeof(SnmpVersionEnum), item)
                });
            }

            var apne = Enum.GetValues(typeof(AssetPropertyNameEnum));
            foreach (var item in apne)
            {
                modelBuilder.Entity<AssetProperty>().HasData(AssetPropertyNameDictionary.Dict[(AssetPropertyNameEnum)item]);
            }
            #endregion

            modelBuilder.Entity<AgentTagSet>()
                .HasData(new AgentTagSet()
                {
                    Id = 1,
                    Name = "Default"
                });

            modelBuilder.Entity<AgentTag>().HasData(
                new AgentTag() {Id = 1, Tagname = "CpuUsage", IsHistorized = true, AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"Processor;% Processor Time;_Total", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 },
                new AgentTag() {Id = 2, Tagname = "MemoryAvailable", IsHistorized = true, AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"Memory;Available MBytes", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 },
                new AgentTag() {Id = 3, Tagname = "MemoryTotal", IsHistorized = true, AgentDataTypeId = (int)AgentDataTypeEnum.WMI, WmiManagementObject = @"TotalPhysicalMemory", ValueDataTypeId = (int)TagDataTypeEnum.Double, AgentTagSetId = 1 },
                new AgentTag() {Id = 4, Tagname = "PhysicalDiskIdleTime", IsHistorized = true, AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"PhysicalDisk;% Idle Time;_Total", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 },
                new AgentTag() {Id = 5, Tagname = "PhysicalDiskWorkTime", IsHistorized = true, AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"PhysicalDisk;% Disk Time;_Total", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 },
                new AgentTag() {Id = 6, Tagname = "LogicalDiskFreeSpace", IsHistorized = true, AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"LogicalDisk;% Free Space;_Total", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 }
                );

            modelBuilder.Entity<SnmpTagSet>()
                .HasData(new SnmpTagSet()
                {
                    Id = 1,
                    Name = "Default",
                    VersionId = (int)SnmpVersionEnum.V2c
                });

            modelBuilder.Entity<SnmpTag>().HasData(
                new SnmpTag() {Id = 1, Tagname= "sysName", OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.5.0", ValueDataTypeId = (int)TagDataTypeEnum.String, SnmpTagSetId = 1 },
                new SnmpTag() {Id = 2, Tagname= "sysDescr", OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.1.0", ValueDataTypeId = (int)TagDataTypeEnum.String, SnmpTagSetId = 1 },
                new SnmpTag() {Id = 3, Tagname= "sysObjectID", OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.2.0", ValueDataTypeId = (int)TagDataTypeEnum.String, SnmpTagSetId = 1 },
                new SnmpTag() {Id = 4, Tagname= "sysUpTime", OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.3.0", ValueDataTypeId = (int)TagDataTypeEnum.String, SnmpTagSetId = 1 },
                new SnmpTag() {Id = 5, Tagname= "sysContact", OperationId = (int)SnmpOperationEnum.Get, OID = "1.3.6.1.2.1.1.4.0", ValueDataTypeId = (int)TagDataTypeEnum.String, SnmpTagSetId = 1 }
                );

            modelBuilder.Entity<Asset>()
                .HasData(new Asset()
                {
                    Id = 1,
                    Name = "AssetMonitorNET Server",
                    IpAddress = "127.0.0.1",
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
