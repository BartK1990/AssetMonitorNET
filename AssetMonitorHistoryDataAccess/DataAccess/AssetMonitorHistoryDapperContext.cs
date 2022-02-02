using Microsoft.Data.SqlClient;
using System.Data;

namespace AssetMonitorHistoryDataAccess.DataAccess
{
    public class AssetMonitorHistoryDapperContext
    {
        private readonly string _connectionString;

        public AssetMonitorHistoryDapperContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
        public SqlConnection CreateSqlConnection() => new SqlConnection(_connectionString);
    }
}
