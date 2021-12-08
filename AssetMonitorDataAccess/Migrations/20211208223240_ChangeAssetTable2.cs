using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class ChangeAssetTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "Assets",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);
        }
    }
}
