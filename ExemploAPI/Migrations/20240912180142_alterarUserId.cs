using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExemploAPI.Migrations
{
    /// <inheritdoc />
    public partial class alterarUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Vendas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_UserId",
                table: "Vendas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_AspNetUsers_UserId",
                table: "Vendas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_AspNetUsers_UserId",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_UserId",
                table: "Vendas");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Vendas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
