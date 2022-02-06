using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class HistoricalNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorizationTagConfig");

            migrationBuilder.DropTable(
                name: "HistorizationType");

            migrationBuilder.CreateTable(
                name: "HistoricalType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalTagConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentTagId = table.Column<int>(nullable: true),
                    SnmpTagId = table.Column<int>(nullable: true),
                    HistorizationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalTagConfig", x => x.Id);
                    table.CheckConstraint("CK_HistorizationTagConfig_AgentOrSnmp", "([AgentTagId] IS NOT NULL AND [SnmpTagId] IS NULL) OR ([AgentTagId] IS NULL AND [SnmpTagId] IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_HistoricalTagConfig_AgentTag_AgentTagId",
                        column: x => x.AgentTagId,
                        principalTable: "AgentTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoricalTagConfig_HistoricalType_HistorizationTypeId",
                        column: x => x.HistorizationTypeId,
                        principalTable: "HistoricalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricalTagConfig_SnmpTag_SnmpTagId",
                        column: x => x.SnmpTagId,
                        principalTable: "SnmpTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "HistoricalType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Last" },
                    { 2, "Maximum" },
                    { 3, "Average" },
                    { 4, "Minimum" }
                });

            migrationBuilder.InsertData(
                table: "HistoricalTagConfig",
                columns: new[] { "Id", "AgentTagId", "HistorizationTypeId", "SnmpTagId" },
                values: new object[,]
                {
                    { 2, 1, 2, null },
                    { 1, 1, 3, null },
                    { 3, 2, 3, null },
                    { 4, 3, 3, null },
                    { 5, 4, 3, null },
                    { 6, 5, 3, null },
                    { 7, 6, 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalTagConfig_HistorizationTypeId",
                table: "HistoricalTagConfig",
                column: "HistorizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalTagConfig_SnmpTagId",
                table: "HistoricalTagConfig",
                column: "SnmpTagId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalTagConfig_AgentTagId_SnmpTagId_HistorizationTypeId",
                table: "HistoricalTagConfig",
                columns: new[] { "AgentTagId", "SnmpTagId", "HistorizationTypeId" },
                unique: true,
                filter: "[AgentTagId] IS NOT NULL AND [SnmpTagId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricalTagConfig");

            migrationBuilder.DropTable(
                name: "HistoricalType");

            migrationBuilder.CreateTable(
                name: "HistorizationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorizationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistorizationTagConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentTagId = table.Column<int>(type: "int", nullable: true),
                    HistorizationTypeId = table.Column<int>(type: "int", nullable: false),
                    SnmpTagId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorizationTagConfig", x => x.Id);
                    table.CheckConstraint("CK_HistorizationTagConfig_AgentOrSnmp", "([AgentTagId] IS NOT NULL AND [SnmpTagId] IS NULL) OR ([AgentTagId] IS NULL AND [SnmpTagId] IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_HistorizationTagConfig_AgentTag_AgentTagId",
                        column: x => x.AgentTagId,
                        principalTable: "AgentTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistorizationTagConfig_HistorizationType_HistorizationTypeId",
                        column: x => x.HistorizationTypeId,
                        principalTable: "HistorizationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistorizationTagConfig_SnmpTag_SnmpTagId",
                        column: x => x.SnmpTagId,
                        principalTable: "SnmpTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "HistorizationType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Last" },
                    { 2, "Maximum" },
                    { 3, "Average" },
                    { 4, "Minimum" }
                });

            migrationBuilder.InsertData(
                table: "HistorizationTagConfig",
                columns: new[] { "Id", "AgentTagId", "HistorizationTypeId", "SnmpTagId" },
                values: new object[,]
                {
                    { 2, 1, 2, null },
                    { 1, 1, 3, null },
                    { 3, 2, 3, null },
                    { 4, 3, 3, null },
                    { 5, 4, 3, null },
                    { 6, 5, 3, null },
                    { 7, 6, 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorizationTagConfig_HistorizationTypeId",
                table: "HistorizationTagConfig",
                column: "HistorizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorizationTagConfig_SnmpTagId",
                table: "HistorizationTagConfig",
                column: "SnmpTagId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorizationTagConfig_AgentTagId_SnmpTagId_HistorizationTypeId",
                table: "HistorizationTagConfig",
                columns: new[] { "AgentTagId", "SnmpTagId", "HistorizationTypeId" },
                unique: true,
                filter: "[AgentTagId] IS NOT NULL AND [SnmpTagId] IS NOT NULL");
        }
    }
}
