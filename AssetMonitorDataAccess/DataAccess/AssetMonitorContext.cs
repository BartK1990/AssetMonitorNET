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
        public DbSet<AssetType> AssetType { get; set; }
        public DbSet<AgentDataType> AgentDataType { get; set; }
        public DbSet<AgentTag> AgentTag { get; set; }
        public DbSet<AgentTagSet> AgentTagSet { get; set; }
        public DbSet<HttpNodeRedTag> HttpNodeRedTag { get; set; }
        public DbSet<HttpNodeRedTagSet> HttpNodeRedTagSet { get; set; }
        public DbSet<SnmpOperation> SnmpOperation { get; set; }
        public DbSet<SnmpTag> SnmpTag { get; set; }
        public DbSet<SnmpTagSet> SnmpTagSet { get; set; }
        public DbSet<TagDataType> TagDataType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AgentTag>()
                .HasIndex(c => new { c.Id, c.Tagname }).IsUnique();
            modelBuilder.Entity<AgentTag>().Property(p => p.ScaleFactor).HasDefaultValue(1.0);
            modelBuilder.Entity<AgentTag>().Property(p => p.ScaleOffset).HasDefaultValue(0.0);

            modelBuilder.Entity<SnmpTag>()
                .HasIndex(c => new { c.Id, c.Tagname }).IsUnique();
            modelBuilder.Entity<SnmpTag>().Property(p => p.ScaleFactor).HasDefaultValue(1.0);
            modelBuilder.Entity<SnmpTag>().Property(p => p.ScaleOffset).HasDefaultValue(0.0);

            modelBuilder.Entity<HttpNodeRedTag>()
                .HasIndex(c => new { c.Id, c.Tagname }).IsUnique();
            modelBuilder.Entity<HttpNodeRedTag>().Property(p => p.ScaleFactor).HasDefaultValue(1.0);
            modelBuilder.Entity<HttpNodeRedTag>().Property(p => p.ScaleOffset).HasDefaultValue(0.0);

            #region Enums
            var ate = Enum.GetValues(typeof(AssetTypeEnum));
            foreach (var item in ate)
            {
                modelBuilder.Entity<AssetType>().HasData(new AssetType()
                {
                    Id = (int)item,
                    Type = Enum.GetName(typeof(AssetTypeEnum), item)
                });
            }

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
            #endregion

            modelBuilder.Entity<AgentTag>(entity =>
                entity.HasCheckConstraint("CK_AgentTag_NotNullTagInfo", 
                $"[{nameof(Models.AgentTag.PerformanceCounter)}] IS NOT NULL OR [{nameof(Models.AgentTag.WmiManagementObject)}] IS NOT NULL OR [{nameof(Models.AgentTag.ServiceName)}] IS NOT NULL"));

            modelBuilder.Entity<AgentTagSet>()
                .HasData(new AgentTagSet()
                {
                    Id = 1,
                    Name = "Windows Default"
                });

            modelBuilder.Entity<AssetProperty>().HasData(
                new AssetProperty() { Id = (int)AssetPropertyNameEnum.AgentTcpPort, Name = AssetPropertyNameEnum.AgentTcpPort.ToString(), Description = "Agent TCP Port", ValueDataTypeId = (int)AssetPropertyDataTypeEnum.Integer }
                );

            modelBuilder.Entity<AgentTag>().HasData(
                new AgentTag() { Id = 1, Tagname = "CpuUsage", AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"Processor;% Processor Time;_Total", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 },
                new AgentTag() { Id = 2, Tagname = "MemoryAvailable", AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"Memory;Available MBytes", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 },
                new AgentTag() { Id = 3, Tagname = "MemoryTotal", AgentDataTypeId = (int)AgentDataTypeEnum.WMI, WmiManagementObject = @"TotalPhysicalMemory", ValueDataTypeId = (int)TagDataTypeEnum.Double, AgentTagSetId = 1 },
                new AgentTag() { Id = 4, Tagname = "PhysicalDiskIdleTime", AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"PhysicalDisk;% Idle Time;_Total", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 },
                new AgentTag() { Id = 5, Tagname = "PhysicalDiskWorkTime", AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"PhysicalDisk;% Disk Time;_Total", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 },
                new AgentTag() { Id = 6, Tagname = "LogicalDiskFreeSpace", AgentDataTypeId = (int)AgentDataTypeEnum.PerformanceCounter, PerformanceCounter = @"LogicalDisk;% Free Space;_Total", ValueDataTypeId = (int)TagDataTypeEnum.Float, AgentTagSetId = 1 }
                );

            modelBuilder.Entity<Asset>()
                .HasData(new Asset()
                {
                    Id = 1,
                    Name = "AssetMonitorNET Server",
                    IpAddress = "127.0.0.1",
                    AssetTypeId = 1,
                    AgentTagSetId = 1
                });
        }
    }
}
