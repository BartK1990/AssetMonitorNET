using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace AssetMonitorDataAccess.DataAccess
{
    public class AssetMonitorContext : DbContext
    {
        public AssetMonitorContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        // ToDo: Add migration!
        //public DbSet<AssetType> AssetTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                    Type = Enum.GetName(typeof(TagDataTypeEnum), item)
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

            modelBuilder.Entity<AgentTag>(entity =>
                entity.HasCheckConstraint("CK_AgentTag_NotNullTagInfo", 
                $"([{nameof(AgentTag.PerformanceCounter)}] IS NOT NULL) OR ([{nameof(AgentTag.WmiManagementObject)}] IS NOT NULL) OR ([{nameof(AgentTag.ServiceName)}] IS NOT NULL)"));

            modelBuilder.Entity<Asset>()
                .HasData(new Asset()
                {
                    Id = 1,
                    Name = "AssetMonitorNET Server",
                    IpAddress = "127.0.0.1",
                    AssetTypeId = 1
                });
        }
    }
}
