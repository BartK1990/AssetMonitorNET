using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class AssetType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssetTypeId",
                table: "Assets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AssetType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetTypeId",
                table: "Assets",
                column: "AssetTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AssetType_AssetTypeId",
                table: "Assets",
                column: "AssetTypeId",
                principalTable: "AssetType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AssetType_AssetTypeId",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "AssetType");

            migrationBuilder.DropIndex(
                name: "IX_Assets_AssetTypeId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AssetTypeId",
                table: "Assets");
        }
    }
}
