using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class IsHistorized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHistorized",
                table: "SnmpTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorized",
                table: "AgentTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsHistorized",
                value: true);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsHistorized",
                value: true);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsHistorized",
                value: true);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsHistorized",
                value: true);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsHistorized",
                value: true);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsHistorized",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHistorized",
                table: "SnmpTag");

            migrationBuilder.DropColumn(
                name: "IsHistorized",
                table: "AgentTag");
        }
    }
}
