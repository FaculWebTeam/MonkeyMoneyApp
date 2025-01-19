using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonkeyMoneyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Metas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Bancos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Metas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bancos");
        }
    }
}
