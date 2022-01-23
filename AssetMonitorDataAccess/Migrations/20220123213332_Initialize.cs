using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgentTagSetId",
                table: "Assets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HttpNodeRedTagSetId",
                table: "Assets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SnmpTagSetId",
                table: "Assets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgentDataType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataType = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentDataType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgentTagSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTagSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HttpNodeRedTagSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpNodeRedTagSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnmpOperation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operation = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnmpOperation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnmpTagSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnmpTagSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagDataType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagDataType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgentTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tagname = table.Column<string>(maxLength: 70, nullable: false),
                    AgentDataTypeId = table.Column<int>(nullable: false),
                    PerformanceCounter = table.Column<string>(maxLength: 200, nullable: true),
                    WmiManagementObject = table.Column<string>(maxLength: 200, nullable: true),
                    ServiceName = table.Column<string>(maxLength: 256, nullable: true),
                    ValueDataTypeId = table.Column<int>(nullable: false),
                    AgentTagSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTag", x => x.Id);
                    table.CheckConstraint("CK_AgentTag_NotNullTagInfo", "([PerformanceCounter] IS NOT NULL) OR ([WmiManagementObject] IS NOT NULL) OR ([ServiceName] IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_AgentTag_AgentDataType_AgentDataTypeId",
                        column: x => x.AgentDataTypeId,
                        principalTable: "AgentDataType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgentTag_AgentTagSet_AgentTagSetId",
                        column: x => x.AgentTagSetId,
                        principalTable: "AgentTagSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgentTag_TagDataType_ValueDataTypeId",
                        column: x => x.ValueDataTypeId,
                        principalTable: "TagDataType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AgentDataType",
                columns: new[] { "Id", "DataType" },
                values: new object[,]
                {
                    { 1, "PerformanceCounter" },
                    { 2, "WMI" },
                    { 3, "ServiceState" }
                });

            migrationBuilder.InsertData(
                table: "SnmpOperation",
                columns: new[] { "Id", "Operation" },
                values: new object[] { 1, "Get" });

            migrationBuilder.InsertData(
                table: "TagDataType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Boolean" },
                    { 2, "Integer" },
                    { 3, "Float" },
                    { 4, "Double" },
                    { 5, "String" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AgentTagSetId",
                table: "Assets",
                column: "AgentTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_HttpNodeRedTagSetId",
                table: "Assets",
                column: "HttpNodeRedTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_SnmpTagSetId",
                table: "Assets",
                column: "SnmpTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTag_AgentDataTypeId",
                table: "AgentTag",
                column: "AgentDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTag_AgentTagSetId",
                table: "AgentTag",
                column: "AgentTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTag_ValueDataTypeId",
                table: "AgentTag",
                column: "ValueDataTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AgentTagSet_AgentTagSetId",
                table: "Assets",
                column: "AgentTagSetId",
                principalTable: "AgentTagSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_HttpNodeRedTagSet_HttpNodeRedTagSetId",
                table: "Assets",
                column: "HttpNodeRedTagSetId",
                principalTable: "HttpNodeRedTagSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_SnmpTagSet_SnmpTagSetId",
                table: "Assets",
                column: "SnmpTagSetId",
                principalTable: "SnmpTagSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AgentTagSet_AgentTagSetId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_HttpNodeRedTagSet_HttpNodeRedTagSetId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_SnmpTagSet_SnmpTagSetId",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "AgentTag");

            migrationBuilder.DropTable(
                name: "HttpNodeRedTagSet");

            migrationBuilder.DropTable(
                name: "SnmpOperation");

            migrationBuilder.DropTable(
                name: "SnmpTagSet");

            migrationBuilder.DropTable(
                name: "AgentDataType");

            migrationBuilder.DropTable(
                name: "AgentTagSet");

            migrationBuilder.DropTable(
                name: "TagDataType");

            migrationBuilder.DropIndex(
                name: "IX_Assets_AgentTagSetId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_HttpNodeRedTagSetId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_SnmpTagSetId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AgentTagSetId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "HttpNodeRedTagSetId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "SnmpTagSetId",
                table: "Assets");
        }
    }
}
