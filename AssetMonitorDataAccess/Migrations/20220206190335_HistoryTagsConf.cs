using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class HistoryTagsConf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SnmpTag_SnmpTagSetId",
                table: "SnmpTag");

            migrationBuilder.DropIndex(
                name: "IX_SnmpTag_Id_Tagname",
                table: "SnmpTag");

            migrationBuilder.DropIndex(
                name: "IX_AgentTag_AgentTagSetId",
                table: "AgentTag");

            migrationBuilder.DropIndex(
                name: "IX_AgentTag_Id_Tagname",
                table: "AgentTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedAvg",
                table: "SnmpTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedLast",
                table: "SnmpTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedMax",
                table: "SnmpTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedMin",
                table: "SnmpTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedAvg",
                table: "AgentTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedLast",
                table: "AgentTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedMax",
                table: "AgentTag");

            migrationBuilder.DropColumn(
                name: "IsHistorizedMin",
                table: "AgentTag");

            migrationBuilder.CreateTable(
                name: "HistorizationType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorizationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistorizationTagConfig",
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
                name: "IX_SnmpTag_SnmpTagSetId_Tagname",
                table: "SnmpTag",
                columns: new[] { "SnmpTagSetId", "Tagname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgentTag_AgentTagSetId_Tagname",
                table: "AgentTag",
                columns: new[] { "AgentTagSetId", "Tagname" },
                unique: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorizationTagConfig");

            migrationBuilder.DropTable(
                name: "HistorizationType");

            migrationBuilder.DropIndex(
                name: "IX_SnmpTag_SnmpTagSetId_Tagname",
                table: "SnmpTag");

            migrationBuilder.DropIndex(
                name: "IX_AgentTag_AgentTagSetId_Tagname",
                table: "AgentTag");

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedAvg",
                table: "SnmpTag",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedLast",
                table: "SnmpTag",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedMax",
                table: "SnmpTag",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedMin",
                table: "SnmpTag",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedAvg",
                table: "AgentTag",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedLast",
                table: "AgentTag",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedMax",
                table: "AgentTag",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistorizedMin",
                table: "AgentTag",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsHistorizedAvg", "IsHistorizedMax" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsHistorizedAvg",
                value: true);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsHistorizedAvg",
                value: true);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsHistorizedAvg",
                value: true);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsHistorizedAvg",
                value: true);

            migrationBuilder.UpdateData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsHistorizedAvg",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_SnmpTag_SnmpTagSetId",
                table: "SnmpTag",
                column: "SnmpTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_SnmpTag_Id_Tagname",
                table: "SnmpTag",
                columns: new[] { "Id", "Tagname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgentTag_AgentTagSetId",
                table: "AgentTag",
                column: "AgentTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTag_Id_Tagname",
                table: "AgentTag",
                columns: new[] { "Id", "Tagname" },
                unique: true);
        }
    }
}
