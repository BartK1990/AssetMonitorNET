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

    }
}
