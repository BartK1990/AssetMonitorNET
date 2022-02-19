using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class AlarmConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlarmType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmTagConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ping = table.Column<bool>(nullable: false),
                    AgentTagId = table.Column<int>(nullable: true),
                    SnmpTagId = table.Column<int>(nullable: true),
                    AlarmTypeId = table.Column<int>(nullable: false),
                    ActivationTime = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmTagConfig", x => x.Id);
                    table.CheckConstraint("CK_AlarmTagConfig_PingOrAgentOrSnmp", "([Ping] = 1 AND [AgentTagId] IS NULL AND [SnmpTagId] IS NULL) OR ([Ping] = 0 AND [AgentTagId] IS NOT NULL AND [SnmpTagId] IS NULL) OR ([Ping] = 0 AND [AgentTagId] IS NULL AND [SnmpTagId] IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_AlarmTagConfig_AgentTag_AgentTagId",
                        column: x => x.AgentTagId,
                        principalTable: "AgentTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmTagConfig_AlarmType_AlarmTypeId",
                        column: x => x.AlarmTypeId,
                        principalTable: "AlarmType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlarmTagConfig_SnmpTag_SnmpTagId",
                        column: x => x.SnmpTagId,
                        principalTable: "SnmpTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AlarmType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Equal" },
                    { 2, "NotEqual" },
                    { 3, "GreaterOrEqual" },
                    { 4, "Greater" },
                    { 5, "LessOrEqual" },
                    { 6, "Less" }
                });

            migrationBuilder.InsertData(
                table: "AlarmTagConfig",
                columns: new[] { "Id", "ActivationTime", "AgentTagId", "AlarmTypeId", "Ping", "SnmpTagId", "Value" },
                values: new object[] { 1, 30, 1, 3, false, null, "50" });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmTagConfig_AgentTagId",
                table: "AlarmTagConfig",
                column: "AgentTagId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmTagConfig_AlarmTypeId",
                table: "AlarmTagConfig",
                column: "AlarmTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmTagConfig_SnmpTagId",
                table: "AlarmTagConfig",
                column: "SnmpTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmTagConfig");

            migrationBuilder.DropTable(
                name: "AlarmType");
        }
    }
}
