using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class PropertyDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AssetProperty",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AssetProperty",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Agent TCP Port");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AssetProperty");
        }
    }
}
