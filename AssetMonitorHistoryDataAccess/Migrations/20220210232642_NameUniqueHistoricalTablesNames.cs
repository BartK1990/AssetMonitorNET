using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorHistoryDataAccess.Migrations
{
    public partial class NameUniqueHistoricalTablesNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HistoricalDataTable",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalDataTable_Name",
                table: "HistoricalDataTable",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HistoricalDataTable_Name",
                table: "HistoricalDataTable");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HistoricalDataTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
