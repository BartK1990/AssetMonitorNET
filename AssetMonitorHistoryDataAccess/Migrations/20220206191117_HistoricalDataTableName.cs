using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorHistoryDataAccess.Migrations
{
    public partial class HistoricalDataTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Asset",
                table: "Asset");

            migrationBuilder.RenameTable(
                name: "Asset",
                newName: "HistoricalDataTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoricalDataTable",
                table: "HistoricalDataTable",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoricalDataTable",
                table: "HistoricalDataTable");

            migrationBuilder.RenameTable(
                name: "HistoricalDataTable",
                newName: "Asset");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asset",
                table: "Asset",
                column: "Id");
        }
    }
}
