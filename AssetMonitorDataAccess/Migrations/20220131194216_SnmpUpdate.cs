using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetMonitorDataAccess.Migrations
{
    public partial class SnmpUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VersionId",
                table: "SnmpTagSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "OID",
                table: "SnmpTag",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Operation",
                table: "SnmpOperation",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

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

            migrationBuilder.UpdateData(
                table: "AgentTagSet",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Default");

            migrationBuilder.InsertData(
                table: "AssetProperty",
                columns: new[] { "Id", "Description", "Name", "ValueDataTypeId" },
                values: new object[,]
                {
                    { 2, "SNMP UDP Port", "SnmpUdpPort", 2 },
                    { 3, "SNMP timeout", "SnmpTimeout", 2 },
                    { 4, "SNMP number of retries", "SnmpRetries", 2 }
                });

            migrationBuilder.InsertData(
                table: "SnmpVersion",
                columns: new[] { "Id", "Version" },
                values: new object[,]
                {
                    { 1, "V1" },
                    { 2, "V2c" }
                });

            migrationBuilder.InsertData(
                table: "AssetPropertyValue",
                columns: new[] { "Id", "AssetId", "AssetPropertyId", "Value" },
                values: new object[,]
                {
                    { -1, 1, 2, "161" },
                    { -2, 1, 3, "3000" },
                    { -3, 1, 4, "1" }
                });

            migrationBuilder.InsertData(
                table: "SnmpTagSet",
                columns: new[] { "Id", "Name", "VersionId" },
                values: new object[] { 1, "Default", 2 });

            migrationBuilder.UpdateData(
                table: "Asset",
                keyColumn: "Id",
                keyValue: 1,
                column: "SnmpTagSetId",
                value: 1);

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

            migrationBuilder.CreateIndex(
                name: "IX_SnmpTagSet_VersionId",
                table: "SnmpTagSet",
                column: "VersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SnmpTagSet_SnmpVersion_VersionId",
                table: "SnmpTagSet",
                column: "VersionId",
                principalTable: "SnmpVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SnmpTagSet_SnmpVersion_VersionId",
                table: "SnmpTagSet");

            migrationBuilder.DropTable(
                name: "SnmpVersion");

            migrationBuilder.DropIndex(
                name: "IX_SnmpTagSet_VersionId",
                table: "SnmpTagSet");

            migrationBuilder.DeleteData(
                table: "AssetPropertyValue",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "AssetPropertyValue",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "AssetPropertyValue",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "SnmpTag",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SnmpTag",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SnmpTag",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SnmpTag",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SnmpTag",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AssetProperty",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AssetProperty",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AssetProperty",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SnmpTagSet",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "VersionId",
                table: "SnmpTagSet");

            migrationBuilder.AlterColumn<string>(
                name: "OID",
                table: "SnmpTag",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Operation",
                table: "SnmpOperation",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.UpdateData(
                table: "Asset",
                keyColumn: "Id",
                keyValue: 1,
                column: "SnmpTagSetId",
                value: null);

            migrationBuilder.UpdateData(
                table: "AgentTagSet",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Windows Default");

            migrationBuilder.UpdateData(
                table: "Asset",
                keyColumn: "Id",
                keyValue: 1,
                column: "SnmpTagSetId",
                value: null);
        }
    }
}
