using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class AssetProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "TagDataType");

            migrationBuilder.AddColumn<string>(
                name: "DataType",
                table: "TagDataType",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

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
                name: "AssetProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
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

            migrationBuilder.InsertData(
                table: "AgentTag",
                columns: new[] { "Id", "AgentDataTypeId", "AgentTagSetId", "PerformanceCounter", "ServiceName", "Tagname", "ValueDataTypeId", "WmiManagementObject" },
                values: new object[,]
                {
                    { 4, 1, 1, "PhysicalDisk;% Idle Time;_Total", null, "PhysicalDiskIdleTime", 3, null },
                    { 5, 1, 1, "PhysicalDisk;% Disk Time;_Total", null, "PhysicalDiskWorkTime", 3, null },
                    { 6, 1, 1, "LogicalDisk;% Free Space;_Total", null, "LogicalDiskFreeSpace", 3, null }
                });

            migrationBuilder.InsertData(
                table: "AssetPropertyDataType",
                columns: new[] { "Id", "DataType" },
                values: new object[,]
                {
                    { 1, "String" },
                    { 2, "Integer" },
                    { 3, "Double" }
                });

            migrationBuilder.UpdateData(
                table: "TagDataType",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataType",
                value: "Boolean");

            migrationBuilder.UpdateData(
                table: "TagDataType",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataType",
                value: "Integer");

            migrationBuilder.UpdateData(
                table: "TagDataType",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataType",
                value: "Float");

            migrationBuilder.UpdateData(
                table: "TagDataType",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataType",
                value: "Double");

            migrationBuilder.UpdateData(
                table: "TagDataType",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataType",
                value: "String");

            migrationBuilder.InsertData(
                table: "AssetProperty",
                columns: new[] { "Id", "Name", "ValueDataTypeId" },
                values: new object[] { 1, "AgentTcpPort", 2 });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetPropertyValue");

            migrationBuilder.DropTable(
                name: "AssetProperty");

            migrationBuilder.DropTable(
                name: "AssetPropertyDataType");

            migrationBuilder.DeleteData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AgentTag",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "TagDataType");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "TagDataType",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "TagDataType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: "Boolean");

            migrationBuilder.UpdateData(
                table: "TagDataType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Type",
                value: "Integer");

            migrationBuilder.UpdateData(
                table: "TagDataType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Type",
                value: "Float");

            migrationBuilder.UpdateData(
                table: "TagDataType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Type",
                value: "Double");

            migrationBuilder.UpdateData(
                table: "TagDataType",
                keyColumn: "Id",
                keyValue: 5,
                column: "Type",
                value: "String");
        }
    }
}
