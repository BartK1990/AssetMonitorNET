using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class AssetType6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AssetType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, "Windows" });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "AssetTypeId", "IpAddress", "Name" },
                values: new object[] { 1, 1, "127.0.0.1", "AssetMonitorNET Server" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AssetType",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
