using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class EmailNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AssetPropertyDataType",
                columns: new[] { "Id", "DataType" },
                values: new object[] { 4, "Boolean" });

            migrationBuilder.InsertData(
                table: "AssetProperty",
                columns: new[] { "Id", "Description", "Name", "ValueDataTypeId" },
                values: new object[] { 6, "Enable or disable email notifications", "EmailNotificationsEnable", 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AssetProperty",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AssetPropertyDataType",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
