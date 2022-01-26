using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.AccesoDatos.Migrations
{
    public partial class AgregarCampoDescripcionArticulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "articulos",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "articulos");
        }
    }
}
