using AssetMonitorDataAccess.DataAccess;
using AssetMonitorDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Data.Repositories
{
    public class AssetMonitorRepository : IAssetMonitorRepository
    {
        private readonly AssetMonitorContext _context;

        public AssetMonitorRepository(AssetMonitorContext context)
        {
            _context = context;
        }

        public IEnumerable<Asset> GetAllAssets()
        {
            return _context.Assets.ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
