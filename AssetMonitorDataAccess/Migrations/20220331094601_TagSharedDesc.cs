using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class TagSharedDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TagShared",
                keyColumn: "Id",
                keyValue: 1,
                column: "Enable",
                value: true);

            migrationBuilder.UpdateData(
                table: "TagShared",
                keyColumn: "Id",
                keyValue: 2,
                column: "Enable",
                value: true);

            migrationBuilder.UpdateData(
                table: "TagShared",
                keyColumn: "Id",
                keyValue: 3,
                column: "Enable",
                value: true);

            migrationBuilder.UpdateData(
                table: "TagShared",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ColumnName", "Enable" },
                values: new object[] { "CPU Usage [%]", true });

            migrationBuilder.UpdateData(
                table: "TagShared",
                keyColumn: "Id",
                keyValue: 5,
                column: "Enable",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TagShared",
                keyColumn: "Id",
                keyValue: 1,
                column: "Enable",
                value: false);

            migrationBuilder.UpdateData(
                table: "TagShared",
                keyColumn: "Id",
                keyValue: 2,
                column: "Enable",
                value: false);

            migrationBuilder.UpdateData(
                table: "TagShared",
                keyColumn: "Id",
                keyValue: 3,
                column: "Enable",
                value: false);

            migrationBuilder.UpdateData(
                table: "TagShared",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ColumnName", "Enable" },
                values: new object[] { "CPU [%]", false });

            migrationBuilder.UpdateData(
                table: "TagShared",
                keyColumn: "Id",
                keyValue: 5,
                column: "Enable",
                value: false);
        }
    }
}
