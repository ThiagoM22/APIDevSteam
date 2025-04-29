using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDevSteam.Migrations
{
    /// <inheritdoc />
    public partial class Correcao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expirado",
                table: "CuponsCarrinhos");

            migrationBuilder.DropColumn(
                name: "LimiteUso",
                table: "CuponsCarrinhos");

            migrationBuilder.AddColumn<int>(
                name: "LimiteUso",
                table: "Cupons",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimiteUso",
                table: "Cupons");

            migrationBuilder.AddColumn<bool>(
                name: "Expirado",
                table: "CuponsCarrinhos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LimiteUso",
                table: "CuponsCarrinhos",
                type: "int",
                nullable: true);
        }
    }
}
