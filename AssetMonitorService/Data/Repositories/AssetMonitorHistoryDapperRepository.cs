using AssetMonitorHistoryDataAccess.DataAccess;
using AssetMonitorService.Data.Repositories.Historical;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Data.Repositories
{
    public class AssetMonitorHistoryDapperRepository : IAssetMonitorHistoryDapperRepository
    {
        private readonly AssetMonitorHistoryDapperContext _context;
        protected readonly ILogger<AssetMonitorHistoryDapperRepository> _logger;

        public AssetMonitorHistoryDapperRepository(AssetMonitorHistoryDapperContext context,
            ILogger<AssetMonitorHistoryDapperRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public async Task<string> GetDbVersion()
        {
            using var connection = _context.CreateSqlConnection();
            var version = await connection.ExecuteScalarAsync<string>(@"SELECT @@VERSION");
            return version;
        }

        public async Task CreateOrUpdateTable(string tableName, IList<TableColumnConfig> columns)
        {
            using var connection = _context.CreateSqlConnection();
            var checkQuery = @$"SELECT * FROM information_schema.tables WHERE table_name = '{tableName}'";
            _logger.LogInformation($"{this.GetType().Name} Db query: {checkQuery}");
            var tableExists = (await connection.QueryAsync<object>(checkQuery)).Any();
            _logger.LogInformation($"{tableName} exists: {tableExists}");

            if (tableExists)
            {
                await CheckAddMissingColumnsAsync(connection, tableName, columns);
                return;
            }

            var query = @$"
                CREATE TABLE [dbo].[{tableName}] (
                [Id] INT IDENTITY (1, 1) NOT NULL";
            foreach (var column in columns)
            {
                query += @$", [{column.Name}] {column.Type} " + (column.IsNull ? @"NULL" : @"NOT NULL");
            }
            query += @")";

            _logger.LogInformation($"{this.GetType().Name} Db query: {query}");
            try
            {
                _ = await connection.ExecuteScalarAsync(query);
            }
            catch (Exception)
            {
                _logger.LogError($"Wrong SQL query for creating table: [{tableName}]");
            }
        }

        private async Task CheckAddMissingColumnsAsync(SqlConnection connection, string tableName, IList<TableColumnConfig> columns)
        {
            if (!columns.Any())
            {
                return;
            }

            var query = @$"
                SELECT [name] FROM sys.columns 
                WHERE Object_ID = Object_ID(N'[dbo].[{tableName}]') 
                AND Name IN (";
            foreach (var col in columns)
            {
                if (columns.First().Equals(col))
                {
                    query += $@"N'{col.Name}'";
                }
                else
                {
                    query += $@", N'{col.Name}'";
                }
            }
            query += @")";

            _logger.LogInformation($"{this.GetType().Name} Db query: {query}");

            var result = await connection.QueryAsync(query);
            var nameList = new List<string>();
            foreach (var rows in result)
            {
                var fields = rows as IDictionary<string, object>;
                nameList.Add((string)fields["name"]);
            }

            if(columns.Count == nameList.Count)
            {
                return;
            }
            var missingColumns = columns.Where(c => !nameList.Contains(c.Name)).ToList();

            var addColumnsQuery = @$"ALTER TABLE {tableName} ADD";
            foreach (var misCol in missingColumns)
            {
                addColumnsQuery += @$" [{misCol.Name}] {misCol.Type} " + (misCol.IsNull ? @"NULL" : @"NOT NULL");
                if (!missingColumns.Last().Equals(misCol))
                {
                    addColumnsQuery += @",";
                }
            }

            _logger.LogInformation($"{this.GetType().Name} Db query: {addColumnsQuery}");
            try
            {
                _ = await connection.ExecuteScalarAsync(addColumnsQuery);
            }
            catch (Exception)
            {
                _logger.LogError($"Wrong SQL query for adding columns to: [{tableName}]");
            }
            _logger.LogInformation($"Columns added to table: [{tableName}]");
        }

        public async Task CreateOrUpdateTimeSeriesTable(string tableName, IList<TableColumnConfig> columns)
        {
            columns.Insert(0, new TableColumnConfig() { Name = "TimeStamp", Type = "DATETIME", IsNull = false });
            await CreateOrUpdateTable(tableName, columns);
        }

        public async Task SelectDynamicExample()
        {
            var query = @"SELECT * FROM Companies";
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
            //Console.WriteLine("'UPDATE' affected rows: {0}", nOfRows);
        }

    }
}
