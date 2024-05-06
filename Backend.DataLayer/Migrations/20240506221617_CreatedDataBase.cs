using System;
using Backend.Core.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreatedDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:coin_type", "unknown,bitcoin,ethereum,litecoin,bitcoin_cash,monero,dash,zcash,vert_coin,bit_shares,factom,nem,dogecoin,maid_safe_coin,digi_byte,nautiluscoin,clams,siacoin,decred,veri_coin,einsteinium")
                .Annotation("Npgsql:Enum:device_type", "unknown,pc,laptop,video_card,asic");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    login = table.Column<string>(type: "text", nullable: true),
                    password_salt = table.Column<string>(type: "text", nullable: true),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    age = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "devices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_name = table.Column<string>(type: "text", nullable: true),
                    device_type = table.Column<DeviceType>(type: "device_type", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_devices", x => x.id);
                    table.ForeignKey(
                        name: "fk_devices_users_owner_id",
                        column: x => x.owner_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "operation_with_coins_dto",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    coin_type = table.Column<CoinType>(type: "coin_type", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    device_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_operation_with_coins_dto", x => x.id);
                    table.ForeignKey(
                        name: "fk_operation_with_coins_dto_devices_device_id",
                        column: x => x.device_id,
                        principalTable: "devices",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_devices_owner_id",
                table: "devices",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_operation_with_coins_dto_device_id",
                table: "operation_with_coins_dto",
                column: "device_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operation_with_coins_dto");

            migrationBuilder.DropTable(
                name: "devices");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
