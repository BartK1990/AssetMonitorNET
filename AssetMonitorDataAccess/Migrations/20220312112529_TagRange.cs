using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class TagRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetTagRange",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RangeMax = table.Column<double>(nullable: false),
                    RangeMin = table.Column<double>(nullable: false),
                    AssetId = table.Column<int>(nullable: true),
                    TagId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTagRange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetTagRange_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetTagRange_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AssetTagRange",
                columns: new[] { "Id", "AssetId", "RangeMax", "RangeMin", "TagId" },
                values: new object[] { 1, 1, 100.0, 0.0, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_AssetTagRange_AssetId",
                table: "AssetTagRange",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTagRange_TagId",
                table: "AssetTagRange",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetTagRange");
        }
    }
}
