using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashPasswordSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "users",
                newName: "password_salt");

            migrationBuilder.AddColumn<string>(
                name: "password_hash",
                table: "users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password_hash",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "password_salt",
                table: "users",
                newName: "password");
        }
    }
}
