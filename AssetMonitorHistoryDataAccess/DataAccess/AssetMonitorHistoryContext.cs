using AssetMonitorHistoryDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetMonitorHistoryDataAccess.DataAccess
{
    public class AssetMonitorHistoryContext : DbContext
    {
        public AssetMonitorHistoryContext(DbContextOptions<AssetMonitorHistoryContext> options) : base(options)
        {
        }

        public DbSet<HistoricalDataTable> Asset { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
