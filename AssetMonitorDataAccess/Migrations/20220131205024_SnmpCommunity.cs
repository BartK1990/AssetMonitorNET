using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class SnmpCommunity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AssetProperty",
                columns: new[] { "Id", "Description", "Name", "ValueDataTypeId" },
                values: new object[] { 5, "SNMP Community String", "SnmpCommunity", 1 });

            migrationBuilder.InsertData(
                table: "AssetPropertyValue",
                columns: new[] { "Id", "AssetId", "AssetPropertyId", "Value" },
                values: new object[] { -4, 1, 5, "public" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AssetPropertyValue",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "AssetProperty",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
