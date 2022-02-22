using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class AlarmFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AlarmTagConfig",
                keyColumn: "Id",
                keyValue: 1,
                column: "Value",
                value: "False");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AlarmTagConfig",
                keyColumn: "Id",
                keyValue: 1,
                column: "Value",
                value: "1");
        }
    }
}
