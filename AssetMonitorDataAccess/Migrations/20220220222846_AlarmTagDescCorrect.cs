using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class AlarmTagDescCorrect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AlarmTagConfig",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "CPU usage is to high!");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AlarmTagConfig",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "CPU is usage to high!");
        }
    }
}
