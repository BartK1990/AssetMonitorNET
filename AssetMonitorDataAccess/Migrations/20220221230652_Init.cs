using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class Init : Migration
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
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTagSet", x => x.Id);
                });

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
                name: "AssetPropertyDataType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataType = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetPropertyDataType", x => x.Id);
                });

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
                name: "IcmpTagSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcmpTagSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnmpOperation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operation = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnmpOperation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnmpVersion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnmpVersion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagDataType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataType = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagDataType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEmailAddressSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEmailAddressSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ValueDataTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetProperty_AssetPropertyDataType_ValueDataTypeId",
                        column: x => x.ValueDataTypeId,
                        principalTable: "AssetPropertyDataType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SnmpTagSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    VersionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnmpTagSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnmpTagSet_SnmpVersion_VersionId",
                        column: x => x.VersionId,
                        principalTable: "SnmpVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgentTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tagname = table.Column<string>(maxLength: 70, nullable: false),
                    ScaleFactor = table.Column<double>(nullable: false, defaultValue: 1.0),
                    ScaleOffset = table.Column<double>(nullable: false, defaultValue: 0.0),
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
                    ScaleFactor = table.Column<double>(nullable: false, defaultValue: 1.0),
                    ScaleOffset = table.Column<double>(nullable: false, defaultValue: 0.0),
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
                name: "IcmpTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tagname = table.Column<string>(maxLength: 70, nullable: false),
                    ValueDataTypeId = table.Column<int>(nullable: false),
                    IcmpTagSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcmpTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IcmpTag_IcmpTagSet_IcmpTagSetId",
                        column: x => x.IcmpTagSetId,
                        principalTable: "IcmpTagSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IcmpTag_TagDataType_ValueDataTypeId",
                        column: x => x.ValueDataTypeId,
                        principalTable: "TagDataType",
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
                    UserEmailAddressSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEmailAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEmailAddress_UserEmailAddressSet_UserEmailAddressSetId",
                        column: x => x.UserEmailAddressSetId,
                        principalTable: "UserEmailAddressSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IpAddress = table.Column<string>(maxLength: 15, nullable: false),
                    IcmpTagSetId = table.Column<int>(nullable: true),
                    AgentTagSetId = table.Column<int>(nullable: true),
                    SnmpTagSetId = table.Column<int>(nullable: true),
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
                        name: "FK_Asset_HttpNodeRedTagSet_HttpNodeRedTagSetId",
                        column: x => x.HttpNodeRedTagSetId,
                        principalTable: "HttpNodeRedTagSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asset_IcmpTagSet_IcmpTagSetId",
                        column: x => x.IcmpTagSetId,
                        principalTable: "IcmpTagSet",
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
                name: "SnmpTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tagname = table.Column<string>(maxLength: 70, nullable: false),
                    ScaleFactor = table.Column<double>(nullable: false, defaultValue: 1.0),
                    ScaleOffset = table.Column<double>(nullable: false, defaultValue: 0.0),
                    OID = table.Column<string>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "AssetPropertyValue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    AssetPropertyId = table.Column<int>(nullable: false),
                    AssetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetPropertyValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetPropertyValue_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetPropertyValue_AssetProperty_AssetPropertyId",
                        column: x => x.AssetPropertyId,
                        principalTable: "AssetProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEmailAssetRel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(nullable: false),
                    UserEmailAddressSetId = table.Column<int>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_UserEmailAssetRel_UserEmailAddressSet_UserEmailAddressSetId",
                        column: x => x.UserEmailAddressSetId,
                        principalTable: "UserEmailAddressSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlarmTagConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IcmpTagId = table.Column<int>(nullable: true),
                    AgentTagId = table.Column<int>(nullable: true),
                    SnmpTagId = table.Column<int>(nullable: true),
                    AlarmTypeId = table.Column<int>(nullable: false),
                    ActivationTime = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmTagConfig", x => x.Id);
                    table.CheckConstraint("CK_AlarmTagConfig_PingOrAgentOrSnmp", "([IcmpTagId] IS NOT NULL AND [AgentTagId] IS NULL AND [SnmpTagId] IS NULL) OR ([IcmpTagId] IS NULL AND [AgentTagId] IS NOT NULL AND [SnmpTagId] IS NULL) OR ([IcmpTagId] IS NULL AND [AgentTagId] IS NULL AND [SnmpTagId] IS NOT NULL)");
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
                        name: "FK_AlarmTagConfig_IcmpTag_IcmpTagId",
                        column: x => x.IcmpTagId,
                        principalTable: "IcmpTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmTagConfig_SnmpTag_SnmpTagId",
                        column: x => x.SnmpTagId,
                        principalTable: "SnmpTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalTagConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentTagId = table.Column<int>(nullable: true),
                    SnmpTagId = table.Column<int>(nullable: true),
                    HistorizationTypeId = table.Column<int>(nullable: false),
                    IcmpTagId = table.Column<int>(nullable: true)
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
                        name: "FK_HistoricalTagConfig_IcmpTag_IcmpTagId",
                        column: x => x.IcmpTagId,
                        principalTable: "IcmpTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoricalTagConfig_SnmpTag_SnmpTagId",
                        column: x => x.SnmpTagId,
                        principalTable: "SnmpTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SnmpAssetValue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false),
                    AssetId = table.Column<int>(nullable: false),
                    SnmpTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnmpAssetValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnmpAssetValue_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SnmpAssetValue_SnmpTag_SnmpTagId",
                        column: x => x.SnmpTagId,
                        principalTable: "SnmpTag",
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
                values: new object[] { 1, "Default" });

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
                table: "AssetPropertyDataType",
                columns: new[] { "Id", "DataType" },
                values: new object[,]
                {
                    { 3, "Double" },
                    { 4, "Boolean" },
                    { 1, "String" },
                    { 2, "Integer" }
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
                table: "IcmpTagSet",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Default" });

            migrationBuilder.InsertData(
                table: "SnmpOperation",
                columns: new[] { "Id", "Operation" },
                values: new object[] { 1, "Get" });

            migrationBuilder.InsertData(
                table: "SnmpVersion",
                columns: new[] { "Id", "Version" },
                values: new object[,]
                {
                    { 1, "V1" },
                    { 2, "V2c" }
                });

            migrationBuilder.InsertData(
                table: "TagDataType",
                columns: new[] { "Id", "DataType" },
                values: new object[,]
                {
                    { 5, "String" },
                    { 1, "Boolean" },
                    { 2, "Integer" },
                    { 3, "Float" },
                    { 4, "Double" },
                    { 6, "Long" }
                });

            migrationBuilder.InsertData(
                table: "AgentTag",
                columns: new[] { "Id", "AgentDataTypeId", "AgentTagSetId", "PerformanceCounter", "ServiceName", "Tagname", "ValueDataTypeId", "WmiManagementObject" },
                values: new object[,]
                {
                    { 1, 1, 1, "Processor;% Processor Time;_Total", null, "CpuUsage", 3, null },
                    { 2, 1, 1, "Memory;Available MBytes", null, "MemoryAvailable", 3, null },
                    { 4, 1, 1, "PhysicalDisk;% Idle Time;_Total", null, "PhysicalDiskIdleTime", 3, null },
                    { 5, 1, 1, "PhysicalDisk;% Disk Time;_Total", null, "PhysicalDiskWorkTime", 3, null },
                    { 6, 1, 1, "LogicalDisk;% Free Space;_Total", null, "LogicalDiskFreeSpace", 3, null },
                    { 3, 2, 1, null, null, "MemoryTotal", 4, "TotalPhysicalMemory" }
                });

            migrationBuilder.InsertData(
                table: "AssetProperty",
                columns: new[] { "Id", "Description", "Name", "ValueDataTypeId" },
                values: new object[,]
                {
                    { 5, "SNMP Community String", "SnmpCommunity", 1 },
                    { 1, "Agent TCP Port", "AgentTcpPort", 2 },
                    { 2, "SNMP UDP Port", "SnmpUdpPort", 2 },
                    { 3, "SNMP timeout", "SnmpTimeout", 2 },
                    { 4, "SNMP number of retries", "SnmpRetries", 2 },
                    { 6, "Enable or disable email notifications", "EmailNotificationsEnable", 4 }
                });

            migrationBuilder.InsertData(
                table: "IcmpTag",
                columns: new[] { "Id", "IcmpTagSetId", "Tagname", "ValueDataTypeId" },
                values: new object[,]
                {
                    { 1, 1, "PingState", 1 },
                    { 2, 1, "PingResponseTime", 6 }
                });

            migrationBuilder.InsertData(
                table: "SnmpTagSet",
                columns: new[] { "Id", "Name", "VersionId" },
                values: new object[] { 1, "Default", 2 });

            migrationBuilder.InsertData(
                table: "AlarmTagConfig",
                columns: new[] { "Id", "ActivationTime", "AgentTagId", "AlarmTypeId", "Description", "IcmpTagId", "SnmpTagId", "Value" },
                values: new object[,]
                {
                    { 1, 30, null, 1, "No ping!", 1, null, "1" },
                    { 2, 30, 1, 3, "CPU usage is to high!", null, null, "50" }
                });

            migrationBuilder.InsertData(
                table: "Asset",
                columns: new[] { "Id", "AgentTagSetId", "HttpNodeRedTagSetId", "IcmpTagSetId", "IpAddress", "Name", "SnmpTagSetId" },
                values: new object[] { 1, 1, null, 1, "127.0.0.1", "AssetMonitorNET Server", 1 });

            migrationBuilder.InsertData(
                table: "HistoricalTagConfig",
                columns: new[] { "Id", "AgentTagId", "HistorizationTypeId", "IcmpTagId", "SnmpTagId" },
                values: new object[,]
                {
                    { 1, 1, 3, null, null },
                    { 2, 1, 2, null, null },
                    { 3, 2, 3, null, null },
                    { 5, 4, 3, null, null },
                    { 6, 5, 3, null, null },
                    { 7, 6, 3, null, null },
                    { 4, 3, 3, null, null }
                });

            migrationBuilder.InsertData(
                table: "SnmpTag",
                columns: new[] { "Id", "OID", "OperationId", "SnmpTagSetId", "Tagname", "ValueDataTypeId" },
                values: new object[,]
                {
                    { 1, "1.3.6.1.2.1.1.5.0", 1, 1, "sysName", 5 },
                    { 2, "1.3.6.1.2.1.1.1.0", 1, 1, "sysDescr", 5 },
                    { 3, "1.3.6.1.2.1.1.2.0", 1, 1, "sysObjectID", 5 },
                    { 4, "1.3.6.1.2.1.1.3.0", 1, 1, "sysUpTime", 5 },
                    { 5, "1.3.6.1.2.1.1.4.0", 1, 1, "sysContact", 5 }
                });

            migrationBuilder.InsertData(
                table: "AssetPropertyValue",
                columns: new[] { "Id", "AssetId", "AssetPropertyId", "Value" },
                values: new object[,]
                {
                    { -1, 1, 2, "161" },
                    { -2, 1, 3, "3000" },
                    { -3, 1, 4, "1" },
                    { -4, 1, 5, "public" }
                });

            migrationBuilder.InsertData(
                table: "HistoricalTagConfig",
                columns: new[] { "Id", "AgentTagId", "HistorizationTypeId", "IcmpTagId", "SnmpTagId" },
                values: new object[] { 8, null, 1, null, 4 });

            migrationBuilder.CreateIndex(
                name: "IX_AgentTag_AgentDataTypeId",
                table: "AgentTag",
                column: "AgentDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTag_ValueDataTypeId",
                table: "AgentTag",
                column: "ValueDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTag_AgentTagSetId_Tagname",
                table: "AgentTag",
                columns: new[] { "AgentTagSetId", "Tagname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlarmTagConfig_AgentTagId",
                table: "AlarmTagConfig",
                column: "AgentTagId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmTagConfig_AlarmTypeId",
                table: "AlarmTagConfig",
                column: "AlarmTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmTagConfig_IcmpTagId",
                table: "AlarmTagConfig",
                column: "IcmpTagId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmTagConfig_SnmpTagId",
                table: "AlarmTagConfig",
                column: "SnmpTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AgentTagSetId",
                table: "Asset",
                column: "AgentTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_HttpNodeRedTagSetId",
                table: "Asset",
                column: "HttpNodeRedTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_IcmpTagSetId",
                table: "Asset",
                column: "IcmpTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_SnmpTagSetId",
                table: "Asset",
                column: "SnmpTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetProperty_ValueDataTypeId",
                table: "AssetProperty",
                column: "ValueDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetPropertyValue_AssetId",
                table: "AssetPropertyValue",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetPropertyValue_AssetPropertyId",
                table: "AssetPropertyValue",
                column: "AssetPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalTagConfig_HistorizationTypeId",
                table: "HistoricalTagConfig",
                column: "HistorizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalTagConfig_IcmpTagId",
                table: "HistoricalTagConfig",
                column: "IcmpTagId");

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
                name: "IX_IcmpTag_IcmpTagSetId",
                table: "IcmpTag",
                column: "IcmpTagSetId");

            migrationBuilder.CreateIndex(
                name: "IX_IcmpTag_ValueDataTypeId",
                table: "IcmpTag",
                column: "ValueDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SnmpAssetValue_AssetId",
                table: "SnmpAssetValue",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_SnmpAssetValue_SnmpTagId",
                table: "SnmpAssetValue",
                column: "SnmpTagId");

            migrationBuilder.CreateIndex(
                name: "IX_SnmpTag_OperationId",
                table: "SnmpTag",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_SnmpTag_ValueDataTypeId",
                table: "SnmpTag",
                column: "ValueDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SnmpTag_SnmpTagSetId_Tagname",
                table: "SnmpTag",
                columns: new[] { "SnmpTagSetId", "Tagname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SnmpTagSet_VersionId",
                table: "SnmpTagSet",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmailAddress_UserEmailAddressSetId",
                table: "UserEmailAddress",
                column: "UserEmailAddressSetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmailAssetRel_AssetId",
                table: "UserEmailAssetRel",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmailAssetRel_UserEmailAddressSetId",
                table: "UserEmailAssetRel",
                column: "UserEmailAddressSetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmTagConfig");

            migrationBuilder.DropTable(
                name: "AssetPropertyValue");

            migrationBuilder.DropTable(
                name: "HistoricalTagConfig");

            migrationBuilder.DropTable(
                name: "HttpNodeRedTag");

            migrationBuilder.DropTable(
                name: "SnmpAssetValue");

            migrationBuilder.DropTable(
                name: "UserEmailAddress");

            migrationBuilder.DropTable(
                name: "UserEmailAssetRel");

            migrationBuilder.DropTable(
                name: "AlarmType");

            migrationBuilder.DropTable(
                name: "AssetProperty");

            migrationBuilder.DropTable(
                name: "AgentTag");

            migrationBuilder.DropTable(
                name: "HistoricalType");

            migrationBuilder.DropTable(
                name: "IcmpTag");

            migrationBuilder.DropTable(
                name: "SnmpTag");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "UserEmailAddressSet");

            migrationBuilder.DropTable(
                name: "AssetPropertyDataType");

            migrationBuilder.DropTable(
                name: "AgentDataType");

            migrationBuilder.DropTable(
                name: "SnmpOperation");

            migrationBuilder.DropTable(
                name: "TagDataType");

            migrationBuilder.DropTable(
                name: "AgentTagSet");

            migrationBuilder.DropTable(
                name: "HttpNodeRedTagSet");

            migrationBuilder.DropTable(
                name: "IcmpTagSet");

            migrationBuilder.DropTable(
                name: "SnmpTagSet");

            migrationBuilder.DropTable(
                name: "SnmpVersion");
        }
    }
}
