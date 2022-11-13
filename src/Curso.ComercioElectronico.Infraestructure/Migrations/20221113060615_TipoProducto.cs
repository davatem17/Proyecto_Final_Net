using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Curso.ComercioElectronico.Infraestructure.Migrations
{
    public partial class TipoProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoProductos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Uso = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Flamable = table.Column<bool>(type: "INTEGER", nullable: false),
                    Fragil = table.Column<bool>(type: "INTEGER", nullable: false),
                    Toxico = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoProductos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoProductos");
        }
    }
}
