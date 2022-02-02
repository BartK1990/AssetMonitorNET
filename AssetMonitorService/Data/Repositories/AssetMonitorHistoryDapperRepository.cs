using AssetMonitorHistoryDataAccess.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Data.Repositories
{
    public class AssetMonitorHistoryDapperRepository : IAssetMonitorHistoryDapperRepository
    {
        private readonly AssetMonitorHistoryDapperContext _context;

        public AssetMonitorHistoryDapperRepository(AssetMonitorHistoryDapperContext context)
        {
            _context = context;
        }

        public async Task<string> GetDbVersion()
        {
            using var connection = _context.CreateSqlConnection();
            var version = await connection.ExecuteScalarAsync<string>("SELECT @@VERSION");
            return version;
        }

        public async Task SelectDynamicExample()
        {
            var query = "SELECT * FROM Companies";
            using var connection = _context.CreateSqlConnection();
            var result = await connection.QueryAsync(query);

            foreach (var rows in result)
            {
                var fields = rows as IDictionary<string, object>;
                var sum = fields["Sum"];

            }
        }

        public async Task UpdateExample()
        {
            using var connection = _context.CreateSqlConnection();
            int nOfRows = await connection.ExecuteAsync("UPDATE dbo.[cars] SET [price] = 52000 WHERE [id] = 1");
            Console.WriteLine("'UPDATE' affected rows: {0}", nOfRows);
        }

    }
}
