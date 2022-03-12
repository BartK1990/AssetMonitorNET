using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class TagShared : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TagSharedSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagSharedSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagShared",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tagname = table.Column<string>(nullable: false),
                    ColumnName = table.Column<string>(nullable: false),
                    TagSharedSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagShared", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagShared_TagSharedSet_TagSharedSetId",
                        column: x => x.TagSharedSetId,
                        principalTable: "TagSharedSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TagSharedSet",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Default" });

            migrationBuilder.InsertData(
                table: "TagShared",
                columns: new[] { "Id", "ColumnName", "TagSharedSetId", "Tagname" },
                values: new object[,]
                {
                    { 1, "Ping", 1, "ICMP.PingState" },
                    { 2, "Ping Time [ms]", 1, "ICMP.PingResponseTime" },
                    { 3, "Up Time", 1, "SNMP.sysUpTime" },
                    { 4, "CPU [%]", 1, "Agent.CpuUsage" },
                    { 5, "Memory left [MB]", 1, "Agent.MemoryAvailable" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagShared_TagSharedSetId",
                table: "TagShared",
                column: "TagSharedSetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagShared");

            migrationBuilder.DropTable(
                name: "TagSharedSet");
        }
    }
}
