﻿using AssetMonitorHistoryDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetMonitorHistoryDataAccess.DataAccess
{
    public class AssetMonitorHistoryContext : DbContext
    {
        public AssetMonitorHistoryContext(DbContextOptions<AssetMonitorHistoryContext> options) : base(options)
        {
        }

        public DbSet<HistoricalDataTable> HistoricalDataTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HistoricalDataTable>()
                .HasIndex(c => new { c.Name }).IsUnique();
        }
    }
}
