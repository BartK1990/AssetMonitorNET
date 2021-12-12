using AssetMonitorDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetMonitorDataAccess.DataAccess
{
    public class AssetMonitorContext : DbContext
    {
        public AssetMonitorContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AssetType>()
                .HasData(new AssetType()
                {
                    Id = 1,
                    Type = "Windows"
                });
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
