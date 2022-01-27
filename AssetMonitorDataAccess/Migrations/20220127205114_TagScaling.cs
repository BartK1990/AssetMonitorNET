using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class TagScaling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ScaleFactor",
                table: "SnmpTag",
                nullable: false,
                defaultValue: 1.0);

            migrationBuilder.AddColumn<double>(
                name: "ScaleOffset",
                table: "SnmpTag",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ScaleFactor",
                table: "HttpNodeRedTag",
                nullable: false,
                defaultValue: 1.0);

            migrationBuilder.AddColumn<double>(
                name: "ScaleOffset",
                table: "HttpNodeRedTag",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ScaleFactor",
                table: "AgentTag",
                nullable: false,
                defaultValue: 1.0);

            migrationBuilder.AddColumn<double>(
                name: "ScaleOffset",
                table: "AgentTag",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScaleFactor",
                table: "SnmpTag");

            migrationBuilder.DropColumn(
                name: "ScaleOffset",
                table: "SnmpTag");

            migrationBuilder.DropColumn(
                name: "ScaleFactor",
                table: "HttpNodeRedTag");

            migrationBuilder.DropColumn(
                name: "ScaleOffset",
                table: "HttpNodeRedTag");

            migrationBuilder.DropColumn(
                name: "ScaleFactor",
                table: "AgentTag");

            migrationBuilder.DropColumn(
                name: "ScaleOffset",
                table: "AgentTag");
        }
    }
}
