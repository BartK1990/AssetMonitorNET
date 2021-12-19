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

            var at = Enum.GetNames(typeof(AssetTypeEnum));
            for (int i = 0; i < at.Length; i++)
            {
                modelBuilder.Entity<AssetType>().HasData(new AssetType()
                {
                    Id = i + 1,
                    Type = at[i]
                });
            }
            //modelBuilder.Entity<AssetType>()
            //    .HasData(new AssetType()
            //    {
            //        Id = 1,
            //        Type = "Windows"
            //    });
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
