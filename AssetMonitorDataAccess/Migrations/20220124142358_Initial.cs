using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IpAddress = table.Column<string>(maxLength: 15, nullable: false),
                    AssetTypeId = table.Column<int>(nullable: false),
                    SnmpTagSetId = table.Column<int>(nullable: true),
                    AgentTagSetId = table.Column<int>(nullable: true),
                    HttpNodeRedTagSetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asset_AgentTagSet_AgentTagSetId",
                        column: x => x.AgentTagSetId,
                        principalTable: "AgentTagSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asset_AssetType_AssetTypeId",
                        column: x => x.AssetTypeId,
                        principalTable: "AssetType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asset_HttpNodeRedTagSet_HttpNodeRedTagSetId",
                        column: x => x.HttpNodeRedTagSetId,
                        principalTable: "HttpNodeRedTagSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asset_SnmpTagSet_SnmpTagSetId",
                        column: x => x.SnmpTagSetId,
                        principalTable: "SnmpTagSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.CheckConstraint("CK_AgentTag_NotNullTagInfo", "[PerformanceCounter] IS NOT NULL OR [WmiManagementObject] IS NOT NULL OR [ServiceName] IS NOT NULL");
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

            migrationBuilder.CreateTable(
                name: "HttpNodeRedTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tagname = table.Column<string>(maxLength: 70, nullable: false),
                    HttpTag = table.Column<string>(maxLength: 200, nullable: true),
                    ValueDataTypeId = table.Column<int>(nullable: false),
                    HttpNodeRedTagSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpNodeRedTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HttpNodeRedTag_HttpNodeRedTagSet_HttpNodeRedTagSetId",
                        column: x => x.HttpNodeRedTagSetId,
                        principalTable: "HttpNodeRedTagSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HttpNodeRedTag_TagDataType_ValueDataTypeId",
                        column: x => x.ValueDataTypeId,
                        principalTable: "TagDataType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SnmpTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tagname = table.Column<string>(maxLength: 70, nullable: false),
                    OID = table.Column<string>(maxLength: 100, nullable: false),
                    OperationId = table.Column<int>(nullable: false),
                    ValueDataTypeId = table.Column<int>(nullable: false),
                    SnmpTagSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnmpTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnmpTag_SnmpOperation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "SnmpOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SnmpTag_SnmpTagSet_SnmpTagSetId",
                        column: x => x.SnmpTagSetId,
                        principalTable: "SnmpTagSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SnmpTag_TagDataType_ValueDataTypeId",
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
                table: "AgentTagSet",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Windows Default" });

            migrationBuilder.InsertData(
                table: "AssetType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Windows" },
                    { 2, "SNMP" }
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

            migrationBuilder.InsertData(
                table: "AgentTag",
                columns: new[] { "Id", "AgentDataTypeId", "AgentTagSetId", "PerformanceCounter", "ServiceName", "Tagname", "ValueDataTypeId", "WmiManagementObject" },
                values: new object[,]
                {
                    { 1, 1, 1, "Processor;% Processor Time;_Total", null, "CpuUsage", 3, null },
                    { 2, 1, 1, "Memory;Available MBytes", null, "MemoryAvailable", 3, null },
                    { 3, 2, 1, null, null, "MemoryTotal", 4, "TotalPhysicalMemory" }
                });

            migrationBuilder.InsertData(
                table: "Asset",
                columns: new[] { "Id", "AgentTagSetId", "AssetTypeId", "HttpNodeRedTagSetId", "IpAddress", "Name", "SnmpTagSetId" },
                values: new object[] { 1, 1, 1, null, "127.0.0.1", "AssetMonitorNET Server", null });

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

            migrationBuilder.CreateIndex(
                name: "IX_AgentTag_Id_Tagname",
                table: "AgentTag",
                columns: new[] { "Id", "Tagname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AgentTagSetId",
                table: "Asset",
                column: "AgentTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AssetTypeId",
                table: "Asset",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_HttpNodeRedTagSetId",
                table: "Asset",
                column: "HttpNodeRedTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_SnmpTagSetId",
                table: "Asset",
                column: "SnmpTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpNodeRedTag_HttpNodeRedTagSetId",
                table: "HttpNodeRedTag",
                column: "HttpNodeRedTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpNodeRedTag_ValueDataTypeId",
                table: "HttpNodeRedTag",
                column: "ValueDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpNodeRedTag_Id_Tagname",
                table: "HttpNodeRedTag",
                columns: new[] { "Id", "Tagname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SnmpTag_OperationId",
                table: "SnmpTag",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_SnmpTag_SnmpTagSetId",
                table: "SnmpTag",
                column: "SnmpTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_SnmpTag_ValueDataTypeId",
                table: "SnmpTag",
                column: "ValueDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SnmpTag_Id_Tagname",
                table: "SnmpTag",
                columns: new[] { "Id", "Tagname" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentTag");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "HttpNodeRedTag");

            migrationBuilder.DropTable(
                name: "SnmpTag");

            migrationBuilder.DropTable(
                name: "AgentDataType");

            migrationBuilder.DropTable(
                name: "AgentTagSet");

            migrationBuilder.DropTable(
                name: "AssetType");

            migrationBuilder.DropTable(
                name: "HttpNodeRedTagSet");

            migrationBuilder.DropTable(
                name: "SnmpOperation");

            migrationBuilder.DropTable(
                name: "SnmpTagSet");

            migrationBuilder.DropTable(
                name: "TagDataType");
        }
    }
}
