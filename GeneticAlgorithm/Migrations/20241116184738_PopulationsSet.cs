using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenAlgo.Migrations
{
    /// <inheritdoc />
    public partial class PopulationsSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrangements_Population_PopulationId",
                table: "Arrangements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Population",
                table: "Population");

            migrationBuilder.RenameTable(
                name: "Population",
                newName: "Populations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Populations",
                table: "Populations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrangements_Populations_PopulationId",
                table: "Arrangements",
                column: "PopulationId",
                principalTable: "Populations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrangements_Populations_PopulationId",
                table: "Arrangements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Populations",
                table: "Populations");

            migrationBuilder.RenameTable(
                name: "Populations",
                newName: "Population");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Population",
                table: "Population",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrangements_Population_PopulationId",
                table: "Arrangements",
                column: "PopulationId",
                principalTable: "Population",
                principalColumn: "Id");
        }
    }
}
