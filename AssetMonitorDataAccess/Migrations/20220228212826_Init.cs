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
                name: "IcmpType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcmpType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnmpCommunicationType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnmpCommunicationType", x => x.Id);
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
                name: "TagSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagSet", x => x.Id);
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
                name: "AgentTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentDataTypeId = table.Column<int>(nullable: false),
                    PerformanceCounter = table.Column<string>(maxLength: 200, nullable: true),
                    WmiManagementObject = table.Column<string>(maxLength: 200, nullable: true),
                    ServiceName = table.Column<string>(maxLength: 256, nullable: true)
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
                name: "IcmpTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IcmpTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcmpTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IcmpTag_IcmpType_IcmpTypeId",
                        column: x => x.IcmpTypeId,
                        principalTable: "IcmpType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SnmpTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OID = table.Column<string>(nullable: false),
                    OperationId = table.Column<int>(nullable: false),
                    SnmpCommunicationTypeId = table.Column<int>(nullable: false)
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
                        name: "FK_SnmpTag_SnmpCommunicationType_SnmpCommunicationTypeId",
                        column: x => x.SnmpCommunicationTypeId,
                        principalTable: "SnmpCommunicationType",
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
                    TagSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asset_TagSet_TagSetId",
                        column: x => x.TagSetId,
                        principalTable: "TagSet",
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
                name: "TagCommunicationRel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IcmpTagId = table.Column<int>(nullable: true),
                    AgentTagId = table.Column<int>(nullable: true),
                    SnmpTagId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagCommunicationRel", x => x.Id);
                    table.CheckConstraint("CK_AlarmTagConfig_PingOrAgentOrSnmp", "([IcmpTagId] IS NOT NULL AND [AgentTagId] IS NULL AND [SnmpTagId] IS NULL) OR ([IcmpTagId] IS NULL AND [AgentTagId] IS NOT NULL AND [SnmpTagId] IS NULL) OR ([IcmpTagId] IS NULL AND [AgentTagId] IS NULL AND [SnmpTagId] IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_TagCommunicationRel_AgentTag_AgentTagId",
                        column: x => x.AgentTagId,
                        principalTable: "AgentTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagCommunicationRel_IcmpTag_IcmpTagId",
                        column: x => x.IcmpTagId,
                        principalTable: "IcmpTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagCommunicationRel_SnmpTag_SnmpTagId",
                        column: x => x.SnmpTagId,
                        principalTable: "SnmpTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tagname = table.Column<string>(maxLength: 70, nullable: false),
                    ScaleFactor = table.Column<double>(nullable: false, defaultValue: 1.0),
                    ScaleOffset = table.Column<double>(nullable: false, defaultValue: 0.0),
                    ValueDataTypeId = table.Column<int>(nullable: false),
                    TagSetId = table.Column<int>(nullable: false),
                    TagCommunicationRelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_TagCommunicationRel_TagCommunicationRelId",
                        column: x => x.TagCommunicationRelId,
                        principalTable: "TagCommunicationRel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tag_TagSet_TagSetId",
                        column: x => x.TagSetId,
                        principalTable: "TagSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tag_TagDataType_ValueDataTypeId",
                        column: x => x.ValueDataTypeId,
                        principalTable: "TagDataType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlarmTagConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagId = table.Column<int>(nullable: false),
                    AlarmTypeId = table.Column<int>(nullable: false),
                    ActivationTime = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmTagConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmTagConfig_AlarmType_AlarmTypeId",
                        column: x => x.AlarmTypeId,
                        principalTable: "AlarmType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlarmTagConfig_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalTagConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagId = table.Column<int>(nullable: false),
                    HistorizationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalTagConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricalTagConfig_HistoricalType_HistorizationTypeId",
                        column: x => x.HistorizationTypeId,
                        principalTable: "HistoricalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricalTagConfig_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
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
                    { 1, "String" },
                    { 2, "Integer" },
                    { 3, "Double" },
                    { 4, "Boolean" }
                });

            migrationBuilder.InsertData(
                table: "HistoricalType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 3, "Average" },
                    { 4, "Minimum" },
                    { 1, "Last" },
                    { 2, "Maximum" }
                });

            migrationBuilder.InsertData(
                table: "IcmpTag",
                columns: new[] { "Id", "IcmpTypeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 1 },
                    { 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "SnmpCommunicationType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Normal" },
                    { 2, "OnDemand" }
                });

            migrationBuilder.InsertData(
                table: "SnmpOperation",
                columns: new[] { "Id", "Operation" },
                values: new object[] { 1, "Get" });

            migrationBuilder.InsertData(
                table: "SnmpVersion",
                columns: new[] { "Id", "Version" },
                values: new object[,]
                {
                    { 2, "V2" },
                    { 1, "V1" }
                });

            migrationBuilder.InsertData(
                table: "TagDataType",
                columns: new[] { "Id", "DataType" },
                values: new object[,]
                {
                    { 1, "Boolean" },
                    { 2, "Integer" },
                    { 3, "Float" },
                    { 4, "Double" },
                    { 5, "String" },
                    { 6, "Long" }
                });

            migrationBuilder.InsertData(
                table: "TagSet",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Windows Agent and SNMP Default" },
                    { 2, "SNMP Default" }
                });

            migrationBuilder.InsertData(
                table: "AgentTag",
                columns: new[] { "Id", "AgentDataTypeId", "PerformanceCounter", "ServiceName", "WmiManagementObject" },
                values: new object[,]
                {
                    { 1, 1, "Processor;% Processor Time;_Total", null, null },
                    { 2, 1, "Memory;Available MBytes", null, null },
                    { 4, 1, "PhysicalDisk;% Idle Time;_Total", null, null },
                    { 5, 1, "PhysicalDisk;% Disk Time;_Total", null, null },
                    { 6, 1, "LogicalDisk;% Free Space;_Total", null, null },
                    { 3, 2, null, null, "TotalPhysicalMemory" }
                });

            migrationBuilder.InsertData(
                table: "Asset",
                columns: new[] { "Id", "IpAddress", "Name", "TagSetId" },
                values: new object[] { 1, "127.0.0.1", "AssetMonitorNET Server", 1 });

            migrationBuilder.InsertData(
                table: "AssetProperty",
                columns: new[] { "Id", "Description", "Name", "ValueDataTypeId" },
                values: new object[,]
                {
                    { 6, "SNMP Version (1 or 2)", "SnmpVersion", 1 },
                    { 1, "Agent TCP Port", "AgentTcpPort", 2 },
                    { 2, "SNMP UDP Port", "SnmpUdpPort", 2 },
                    { 3, "SNMP timeout", "SnmpTimeout", 2 },
                    { 4, "SNMP number of retries", "SnmpRetries", 2 },
                    { 7, "Enable or disable email notifications", "EmailNotificationsEnable", 4 },
                    { 5, "SNMP Community String", "SnmpCommunity", 1 }
                });

            migrationBuilder.InsertData(
                table: "SnmpTag",
                columns: new[] { "Id", "OID", "OperationId", "SnmpCommunicationTypeId" },
                values: new object[,]
                {
                    { 9, "1.3.6.1.2.1.1.3.0", 1, 0 },
                    { 8, "1.3.6.1.2.1.1.2.0", 1, 0 },
                    { 7, "1.3.6.1.2.1.1.1.0", 1, 0 },
                    { 6, "1.3.6.1.2.1.1.5.0", 1, 0 },
                    { 5, "1.3.6.1.2.1.1.4.0", 1, 0 },
                    { 4, "1.3.6.1.2.1.1.3.0", 1, 0 },
                    { 1, "1.3.6.1.2.1.1.5.0", 1, 0 },
                    { 2, "1.3.6.1.2.1.1.1.0", 1, 0 },
                    { 10, "1.3.6.1.2.1.1.4.0", 1, 0 },
                    { 3, "1.3.6.1.2.1.1.2.0", 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "TagCommunicationRel",
                columns: new[] { "Id", "AgentTagId", "IcmpTagId", "SnmpTagId" },
                values: new object[,]
                {
                    { 15, null, 4, null },
                    { 14, null, 3, null },
                    { 2, null, 2, null },
                    { 1, null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "AssetPropertyValue",
                columns: new[] { "Id", "AssetId", "AssetPropertyId", "Value" },
                values: new object[,]
                {
                    { -5, 1, 6, "2" },
                    { -3, 1, 4, "1" },
                    { -2, 1, 3, "3000" },
                    { -1, 1, 2, "161" },
                    { -4, 1, 5, "public" }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "TagCommunicationRelId", "TagSetId", "Tagname", "ValueDataTypeId" },
                values: new object[,]
                {
                    { 1, 1, 1, "ICMP.PingState", 1 },
                    { 2, 2, 1, "ICMP.PingResponseTime", 6 },
                    { 14, 14, 2, "ICMP.PingState", 1 },
                    { 15, 15, 2, "ICMP.PingResponseTime", 6 }
                });

            migrationBuilder.InsertData(
                table: "TagCommunicationRel",
                columns: new[] { "Id", "AgentTagId", "IcmpTagId", "SnmpTagId" },
                values: new object[,]
                {
                    { 20, null, null, 10 },
                    { 19, null, null, 9 },
                    { 18, null, null, 8 },
                    { 17, null, null, 7 },
                    { 16, null, null, 6 },
                    { 11, null, null, 3 },
                    { 12, null, null, 4 },
                    { 10, null, null, 2 },
                    { 9, null, null, 1 },
                    { 5, 3, null, null },
                    { 8, 6, null, null },
                    { 7, 5, null, null },
                    { 6, 4, null, null },
                    { 4, 2, null, null },
                    { 13, null, null, 5 },
                    { 3, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "AlarmTagConfig",
                columns: new[] { "Id", "ActivationTime", "AlarmTypeId", "Description", "TagId", "Value" },
                values: new object[,]
                {
                    { 1, 30, 1, "No ping!", 1, "False" },
                    { 3, 30, 1, "No ping!", 14, "False" }
                });

            migrationBuilder.InsertData(
                table: "HistoricalTagConfig",
                columns: new[] { "Id", "HistorizationTypeId", "TagId" },
                values: new object[,]
                {
                    { 11, 1, 14 },
                    { 1, 1, 1 },
                    { 2, 3, 2 },
                    { 12, 3, 15 }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "TagCommunicationRelId", "TagSetId", "Tagname", "ValueDataTypeId" },
                values: new object[,]
                {
                    { 18, 18, 2, "SNMP.sysObjectID", 5 },
                    { 17, 17, 2, "SNMP.sysDescr", 5 },
                    { 16, 16, 2, "SNMP.sysName", 5 },
                    { 13, 13, 1, "SNMP.sysContact", 5 },
                    { 12, 12, 1, "SNMP.sysUpTime", 5 },
                    { 11, 11, 1, "SNMP.sysObjectID", 5 },
                    { 10, 10, 1, "SNMP.sysDescr", 5 },
                    { 3, 3, 1, "Agent.CpuUsage", 3 },
                    { 19, 19, 2, "SNMP.sysUpTime", 5 },
                    { 5, 5, 1, "Agent.MemoryTotal", 4 },
                    { 8, 8, 1, "Agent.LogicalDiskFreeSpace", 3 },
                    { 7, 7, 1, "Agent.PhysicalDiskWorkTime", 3 },
                    { 6, 6, 1, "Agent.PhysicalDiskIdleTime", 3 },
                    { 4, 4, 1, "Agent.MemoryAvailable", 3 },
                    { 9, 9, 1, "SNMP.sysName", 5 },
                    { 20, 20, 2, "SNMP.sysContact", 5 }
                });

            migrationBuilder.InsertData(
                table: "AlarmTagConfig",
                columns: new[] { "Id", "ActivationTime", "AlarmTypeId", "Description", "TagId", "Value" },
                values: new object[] { 2, 30, 3, "CPU usage is to high!", 3, "50" });

            migrationBuilder.InsertData(
                table: "HistoricalTagConfig",
                columns: new[] { "Id", "HistorizationTypeId", "TagId" },
                values: new object[,]
                {
                    { 3, 3, 3 },
                    { 4, 2, 3 },
                    { 5, 3, 4 },
                    { 7, 3, 6 },
                    { 8, 3, 7 },
                    { 9, 3, 8 },
                    { 6, 3, 5 },
                    { 10, 3, 12 },
                    { 13, 1, 19 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentTag_AgentDataTypeId",
                table: "AgentTag",
                column: "AgentDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmTagConfig_AlarmTypeId",
                table: "AlarmTagConfig",
                column: "AlarmTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmTagConfig_TagId_AlarmTypeId",
                table: "AlarmTagConfig",
                columns: new[] { "TagId", "AlarmTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_TagSetId",
                table: "Asset",
                column: "TagSetId");

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
                name: "IX_HistoricalTagConfig_TagId_HistorizationTypeId",
                table: "HistoricalTagConfig",
                columns: new[] { "TagId", "HistorizationTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IcmpTag_IcmpTypeId",
                table: "IcmpTag",
                column: "IcmpTypeId");

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
                name: "IX_SnmpTag_SnmpCommunicationTypeId",
                table: "SnmpTag",
                column: "SnmpCommunicationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_TagCommunicationRelId",
                table: "Tag",
                column: "TagCommunicationRelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ValueDataTypeId",
                table: "Tag",
                column: "ValueDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_TagSetId_Tagname",
                table: "Tag",
                columns: new[] { "TagSetId", "Tagname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagCommunicationRel_AgentTagId",
                table: "TagCommunicationRel",
                column: "AgentTagId",
                unique: true,
                filter: "[AgentTagId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TagCommunicationRel_IcmpTagId",
                table: "TagCommunicationRel",
                column: "IcmpTagId",
                unique: true,
                filter: "[IcmpTagId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TagCommunicationRel_SnmpTagId",
                table: "TagCommunicationRel",
                column: "SnmpTagId",
                unique: true,
                filter: "[SnmpTagId] IS NOT NULL");

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
                name: "SnmpAssetValue");

            migrationBuilder.DropTable(
                name: "SnmpVersion");

            migrationBuilder.DropTable(
                name: "UserEmailAddress");

            migrationBuilder.DropTable(
                name: "UserEmailAssetRel");

            migrationBuilder.DropTable(
                name: "AlarmType");

            migrationBuilder.DropTable(
                name: "AssetProperty");

            migrationBuilder.DropTable(
                name: "HistoricalType");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "UserEmailAddressSet");

            migrationBuilder.DropTable(
                name: "AssetPropertyDataType");

            migrationBuilder.DropTable(
                name: "TagCommunicationRel");

            migrationBuilder.DropTable(
                name: "TagDataType");

            migrationBuilder.DropTable(
                name: "TagSet");

            migrationBuilder.DropTable(
                name: "AgentTag");

            migrationBuilder.DropTable(
                name: "IcmpTag");

            migrationBuilder.DropTable(
                name: "SnmpTag");

            migrationBuilder.DropTable(
                name: "AgentDataType");

            migrationBuilder.DropTable(
                name: "IcmpType");

            migrationBuilder.DropTable(
                name: "SnmpOperation");

            migrationBuilder.DropTable(
                name: "SnmpCommunicationType");
        }
    }
}
