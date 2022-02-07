using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class TagType_Long : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TagDataType",
                columns: new[] { "Id", "DataType" },
                values: new object[] { 6, "Long" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TagDataType",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
