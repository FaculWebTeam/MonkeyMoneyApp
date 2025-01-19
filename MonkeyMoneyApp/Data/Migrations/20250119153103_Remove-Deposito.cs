using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonkeyMoneyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDeposito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deposito",
                table: "Metas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Deposito",
                table: "Metas",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
