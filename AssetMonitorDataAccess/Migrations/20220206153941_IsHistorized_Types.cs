using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class IsHistorized_Types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHistorized",
                table: "SnmpTag");

            migrationBuilder.DropColumn(
                name: "IsHistorized",
                table: "AgentTag");

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedAvg",
                table: "SnmpTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedLast",
                table: "SnmpTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedMax",
                table: "SnmpTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedMin",
                table: "SnmpTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedAvg",
                table: "AgentTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedLast",
                table: "AgentTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedMax",
                table: "AgentTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedMin",
                table: "AgentTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsHistorizedAvg", "IsHistorizedMax" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsHistorizedAvg",
                value: true);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsHistorizedAvg",
                value: true);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsHistorizedAvg",
                value: true);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsHistorizedAvg",
                value: true);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsHistorizedAvg",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHistorizedAvg",
                table: "SnmpTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedLast",
                table: "SnmpTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedMax",
                table: "SnmpTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedMin",
                table: "SnmpTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedAvg",
                table: "AgentTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedLast",
                table: "AgentTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedMax",
                table: "AgentTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedMin",
                table: "AgentTag");

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorized",
                table: "SnmpTag",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorized",
                table: "AgentTag",
                type: "bit",
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
    }
}
