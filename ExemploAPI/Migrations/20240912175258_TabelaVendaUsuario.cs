using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExemploAPI.Migrations
{
    /// <inheritdoc />
    public partial class TabelaVendaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Vendas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vendas");
        }
    }
}
