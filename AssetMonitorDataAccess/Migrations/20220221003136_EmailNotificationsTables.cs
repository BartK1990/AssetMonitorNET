using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class EmailNotificationsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserEmailAssetRel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEmailAssetRel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEmailAssetRel_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEmailAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    UserEmailAssetRelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEmailAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEmailAddress_UserEmailAssetRel_UserEmailAssetRelId",
                        column: x => x.UserEmailAssetRelId,
                        principalTable: "UserEmailAssetRel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEmailAddress_UserEmailAssetRelId",
                table: "UserEmailAddress",
                column: "UserEmailAssetRelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmailAssetRel_AssetId",
                table: "UserEmailAssetRel",
                column: "AssetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEmailAddress");

            migrationBuilder.DropTable(
                name: "UserEmailAssetRel");
        }
    }
}
