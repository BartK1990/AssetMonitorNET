using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class ApplicationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                table: "AssetPropertyDataType",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.CreateTable(
                name: "ApplicationPropertyDataType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPropertyDataType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationProperty",
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
                    table.PrimaryKey("PK_ApplicationProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationProperty_ApplicationPropertyDataType_ValueDataTypeId",
                        column: x => x.ValueDataTypeId,
                        principalTable: "ApplicationPropertyDataType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationPropertyValue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    ApplicationPropertyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPropertyValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationPropertyValue_ApplicationProperty_ApplicationPropertyId",
                        column: x => x.ApplicationPropertyId,
                        principalTable: "ApplicationProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationPropertyDataType",
                columns: new[] { "Id", "DataType" },
                values: new object[,]
                {
                    { 1, "String" },
                    { 2, "Integer" },
                    { 3, "Double" },
                    { 4, "Boolean" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationProperty",
                columns: new[] { "Id", "Description", "Name", "ValueDataTypeId" },
                values: new object[,]
                {
                    { 1, "Scan time of ICMP data Service", "AssetsTimedIcmpDataScanTime", 2 },
                    { 2, "Scan time of Agent/Performance data Service", "AssetsTimedPerformanceDataScanTime", 2 },
                    { 3, "Scan time of SNMP data Service", "AssetsTimedSnmpDataScanTime", 2 },
                    { 4, "Scan time of Notifications Service", "AssetsNotificationTimedScanTime", 2 },
                    { 5, "Scan time of Alarms Service", "AssetsAlarmTimedScanTime", 2 },
                    { 6, "Scan time of Historical data Service", "AssetsHistoryTimedScanTime", 2 },
                    { 7, "Scan time for reading data from main Windows Service", "FrontEndScanTime", 2 }
                });

            migrationBuilder.InsertData(
                table: "ApplicationPropertyValue",
                columns: new[] { "Id", "ApplicationPropertyId", "Value" },
                values: new object[,]
                {
                    { 4, 1, "10" },
                    { 5, 2, "10" },
                    { 6, 3, "10" },
                    { 3, 4, "10" },
                    { 1, 5, "10" },
                    { 2, 6, "10" },
                    { 7, 7, "10" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationProperty_ValueDataTypeId",
                table: "ApplicationProperty",
                column: "ValueDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationPropertyValue_ApplicationPropertyId",
                table: "ApplicationPropertyValue",
                column: "ApplicationPropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationPropertyValue");

            migrationBuilder.DropTable(
                name: "ApplicationProperty");

            migrationBuilder.DropTable(
                name: "ApplicationPropertyDataType");

            migrationBuilder.AlterColumn<string>(
                name: "DataType",
                table: "AssetPropertyDataType",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
