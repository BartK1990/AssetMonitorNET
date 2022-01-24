using Microsoft.EntityFrameworkCore;

namespace AssetMonitorHistoryDataAccess.DataAccess
{
    public class AssetMonitorHistoryContext : DbContext
    {
        public AssetMonitorHistoryContext(DbContextOptions<AssetMonitorHistoryContext> options) : base(options)
        {
        }
    }
}
