using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class ApplicationProperties2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationPropertyValue_ApplicationPropertyId",
                table: "ApplicationPropertyValue");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationPropertyValue_ApplicationPropertyId",
                table: "ApplicationPropertyValue",
                column: "ApplicationPropertyId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationPropertyValue_ApplicationPropertyId",
                table: "ApplicationPropertyValue");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationPropertyValue_ApplicationPropertyId",
                table: "ApplicationPropertyValue",
                column: "ApplicationPropertyId");
        }
    }
}
