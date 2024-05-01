using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPasswordColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "users",
                type: "text",
                nullable: true);
        }
    }
}
