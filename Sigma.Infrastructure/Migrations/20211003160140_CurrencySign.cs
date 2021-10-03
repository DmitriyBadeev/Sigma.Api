using Microsoft.EntityFrameworkCore.Migrations;

namespace Sigma.Infrastructure.Migrations
{
    public partial class CurrencySign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sign",
                table: "Currencies",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sign",
                table: "Currencies");
        }
    }
}
