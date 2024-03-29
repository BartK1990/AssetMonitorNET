﻿using AssetMonitorHistoryDataAccess.DataAccess;
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

        public async Task<string> GetDbVersionAsync()
        {
            using var connection = _context.CreateSqlConnection();
            var version = await connection.ExecuteScalarAsync<string>(@"SELECT @@VERSION");
            return version;
        }

        public async Task CreateOrUpdateTableAsync(string tableName, IList<TableColumnConfig> columns)
        {
            using var connection = _context.CreateSqlConnection();
            var checkQuery = @$"SELECT * FROM information_schema.tables WHERE table_name = '{tableName}'";
            _logger.LogInformation($"{this.GetType().Name} Db query: {checkQuery}");
            var tableExists = (await connection.QueryAsync<object>(checkQuery))?.Any() ?? false;
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
            catch
            {
                _logger.LogError($"Wrong SQL query for creating table: [{tableName}]");
            }
        }

        private async Task CheckAddMissingColumnsAsync(SqlConnection connection, string tableName, IList<TableColumnConfig> columns)
        {
            if (!columns?.Any() ?? true)
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
            catch
            {
                _logger.LogError($"Wrong SQL query for adding columns to: [{tableName}]");
            }
            _logger.LogInformation($"Columns added to table: [{tableName}]");
        }

        public async Task InsertToTableAsync(string tableName, IList<TableColumnValue> columns)
        {
            using var connection = _context.CreateSqlConnection();

            var query = @$"
                INSERT INTO [dbo].[{tableName}] (";
            foreach (var column in columns)
            {
                if (columns.First().Equals(column))
                {
                    query += $@"[{column.Name}]";
                }
                else
                {
                    query += $@", [{column.Name}]";
                }
            }
            query += @") 
                VALUES (";
            foreach (var column in columns)
            {
                if (!columns.First().Equals(column))
                {
                    query += @",";
                }
                if (column.Value == "NULL")
                {
                    query += $@"{column.Value}";
                }
                else 
                {
                    query += $@"N'{column.Value}'";
                }
            }
            query += @")";

            _logger.LogInformation($"{ this.GetType().Name} Db query: {query}");
            try
            {
                _ = await connection.ExecuteScalarAsync(query);
            }
            catch
            {
                _logger.LogError($"Wrong SQL query for creating table: [{tableName}]");
            }
        }

        public async Task InsertToTimeSeriesTableAsync(string tableName, IList<TableColumnValue> columns, string timeStamp)
        {
            if (!DateTime.TryParse(timeStamp, out _))
            {
                throw new ArgumentException("Wrong timestamp format provided for method");
            }

            columns.Insert(0, new TableColumnValue() { Name = "TimeStamp", Value = timeStamp });
            await InsertToTableAsync(tableName, columns);
        }

        public async Task CreateOrUpdateTimeSeriesTableAsync(string tableName, IList<TableColumnConfig> columns)
        {
            columns.Insert(0, new TableColumnConfig() { Name = "TimeStamp", Type = "DATETIME", IsNull = false });
            await CreateOrUpdateTableAsync(tableName, columns);
        }

        public async Task SelectDynamicExampleAsync()
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

        public async Task UpdateExampleAsync()
        {
            using var connection = _context.CreateSqlConnection();
            int nOfRows = await connection.ExecuteAsync("UPDATE dbo.[cars] SET [price] = 52000 WHERE [id] = 1");
            //Console.WriteLine("'UPDATE' affected rows: {0}", nOfRows);
        }

    }
}
