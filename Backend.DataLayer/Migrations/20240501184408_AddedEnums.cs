using System;
using Backend.Core.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DeviceType>(
                name: "device_type",
                table: "devices",
                type: "device_type",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<CoinType>(
                name: "coin_type",
                table: "coins",
                type: "coin_type",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "devices",
                columns: new[] { "id", "address", "device_name", "device_type", "owner_id" },
                values: new object[,]
                {
                    { new Guid("1813948c-4505-44aa-b6d3-c4af1c22d546"), null, "PC", DeviceType.Unknown, null },
                    { new Guid("2025d226-f7cf-4cb9-b66f-1ae1b08b362d"), null, "VideoCard", DeviceType.Unknown, null },
                    { new Guid("397cc8aa-93ec-4021-823b-091ca07dda71"), null, "Laptop", DeviceType.Unknown, null },
                    { new Guid("6dfbf34a-4cbb-45eb-ba7b-77bde8fcf643"), null, "Unknown", DeviceType.Unknown, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "devices",
                keyColumn: "id",
                keyValue: new Guid("1813948c-4505-44aa-b6d3-c4af1c22d546"));

            migrationBuilder.DeleteData(
                table: "devices",
                keyColumn: "id",
                keyValue: new Guid("2025d226-f7cf-4cb9-b66f-1ae1b08b362d"));

            migrationBuilder.DeleteData(
                table: "devices",
                keyColumn: "id",
                keyValue: new Guid("397cc8aa-93ec-4021-823b-091ca07dda71"));

            migrationBuilder.DeleteData(
                table: "devices",
                keyColumn: "id",
                keyValue: new Guid("6dfbf34a-4cbb-45eb-ba7b-77bde8fcf643"));

            migrationBuilder.AlterColumn<int>(
                name: "device_type",
                table: "devices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(DeviceType),
                oldType: "device_type");

            migrationBuilder.AlterColumn<int>(
                name: "coin_type",
                table: "coins",
                type: "integer",
                nullable: false,
                oldClrType: typeof(CoinType),
                oldType: "coin_type");
        }
    }
}
