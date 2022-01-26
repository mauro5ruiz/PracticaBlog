using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.AccesoDatos.Migrations
{
    public partial class ModificarTablaCategoriaAtribOrden : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Orden",
                table: "categorias",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Orden",
                table: "categorias",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
