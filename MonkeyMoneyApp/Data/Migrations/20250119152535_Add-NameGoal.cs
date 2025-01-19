using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonkeyMoneyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNameGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Metas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Metas");
        }
    }
}
