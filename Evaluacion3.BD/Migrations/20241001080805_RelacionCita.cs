using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evaluacion3.BD.Migrations
{
    /// <inheritdoc />
    public partial class RelacionCita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Citas_ClienteId",
                table: "Citas");

            migrationBuilder.CreateIndex(
                name: "Cita_UQ",
                table: "Citas",
                columns: new[] { "ClienteId", "DisponibilidadId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Cita_UQ",
                table: "Citas");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_ClienteId",
                table: "Citas",
                column: "ClienteId");
        }
    }
}
