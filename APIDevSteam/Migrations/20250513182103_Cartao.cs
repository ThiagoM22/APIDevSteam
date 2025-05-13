using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDevSteam.Migrations
{
    /// <inheritdoc />
    public partial class Cartao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cartaoes",
                columns: table => new
                {
                    CartaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeImpresso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroMascarado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bandeira = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimosDigitos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Validade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenCartao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartaoes", x => x.CartaoId);
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    PagamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarrinhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoAutorizacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.PagamentoId);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Carrinhos_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carrinhos",
                        principalColumn: "CarrinhoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Cartaoes_CartaoId",
                        column: x => x.CartaoId,
                        principalTable: "Cartaoes",
                        principalColumn: "CartaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_CarrinhoId",
                table: "Pagamentos",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_CartaoId",
                table: "Pagamentos",
                column: "CartaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "Cartaoes");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "AspNetUsers");
        }
    }
}
