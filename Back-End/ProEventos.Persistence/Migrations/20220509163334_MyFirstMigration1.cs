using Microsoft.EntityFrameworkCore.Migrations;

namespace ProEventos.Persistence.Migrations
{
    public partial class MyFirstMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Palestrantes_PalestranteID",
                table: "RedesSociais");

            migrationBuilder.AlterColumn<int>(
                name: "EventoID",
                table: "RedesSociais",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Palestrantes_PalestranteID",
                table: "RedesSociais",
                column: "PalestranteID",
                principalTable: "Palestrantes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Palestrantes_PalestranteID",
                table: "RedesSociais");

            migrationBuilder.AlterColumn<int>(
                name: "EventoID",
                table: "RedesSociais",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Palestrantes_PalestranteID",
                table: "RedesSociais",
                column: "PalestranteID",
                principalTable: "Palestrantes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
