using AssetMonitorDataAccess.Models;
using System.Collections.Generic;

namespace AspMVC_Monitor.Data.Repositories
{
    public interface IAssetMonitorRepository
    {
        IEnumerable<Asset> GetAllAssets();
        bool SaveAll();
    }
}