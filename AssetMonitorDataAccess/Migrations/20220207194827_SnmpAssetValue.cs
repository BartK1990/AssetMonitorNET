using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class SnmpAssetValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SnmpAssetValue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false),
                    AssetId = table.Column<int>(nullable: false),
                    SnmpTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnmpAssetValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnmpAssetValue_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SnmpAssetValue_SnmpTag_SnmpTagId",
                        column: x => x.SnmpTagId,
                        principalTable: "SnmpTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SnmpAssetValue_AssetId",
                table: "SnmpAssetValue",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_SnmpAssetValue_SnmpTagId",
                table: "SnmpAssetValue",
                column: "SnmpTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnmpAssetValue");
        }
    }
}
