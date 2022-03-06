using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class PropertiesUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AssetPropertyValue_AssetId",
                table: "AssetPropertyValue");

            migrationBuilder.CreateIndex(
                name: "IX_AssetPropertyValue_AssetId_AssetPropertyId",
                table: "AssetPropertyValue",
                columns: new[] { "AssetId", "AssetPropertyId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AssetPropertyValue_AssetId_AssetPropertyId",
                table: "AssetPropertyValue");

            migrationBuilder.CreateIndex(
                name: "IX_AssetPropertyValue_AssetId",
                table: "AssetPropertyValue",
                column: "AssetId");
        }
    }
}
