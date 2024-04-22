using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CoinsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "coins",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    coin_name = table.Column<string>(type: "text", nullable: true),
                    coin_type = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<string>(type: "text", nullable: true),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_coins", x => x.id);
                    table.ForeignKey(
                        name: "fk_coins_users_owner_id",
                        column: x => x.owner_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_coins_owner_id",
                table: "coins",
                column: "owner_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "coins");
        }
    }
}
