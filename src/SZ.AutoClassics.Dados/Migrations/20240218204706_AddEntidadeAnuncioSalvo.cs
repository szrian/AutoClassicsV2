using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SZ.AutoClassics.Dados.Migrations
{
    public partial class AddEntidadeAnuncioSalvo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnunciosSalvos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnuncioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnunciosSalvos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnunciosSalvos_Anuncios_AnuncioId",
                        column: x => x.AnuncioId,
                        principalTable: "Anuncios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnunciosSalvos_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnunciosSalvos_AnuncioId",
                table: "AnunciosSalvos",
                column: "AnuncioId");

            migrationBuilder.CreateIndex(
                name: "IX_AnunciosSalvos_UsuarioId",
                table: "AnunciosSalvos",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnunciosSalvos");
        }
    }
}
