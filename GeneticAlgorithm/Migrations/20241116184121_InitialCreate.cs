using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenAlgo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Population",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Population", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Arrangements",
                columns: table => new
                {
                    ArrangementId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PopulationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arrangements", x => x.ArrangementId);
                    table.ForeignKey(
                        name: "FK_Arrangements_Population_PopulationId",
                        column: x => x.PopulationId,
                        principalTable: "Population",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Square",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    X = table.Column<int>(type: "INTEGER", nullable: false),
                    Y = table.Column<int>(type: "INTEGER", nullable: false),
                    Size = table.Column<int>(type: "INTEGER", nullable: false),
                    ArrangementId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Square", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Square_Arrangements_ArrangementId",
                        column: x => x.ArrangementId,
                        principalTable: "Arrangements",
                        principalColumn: "ArrangementId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arrangements_PopulationId",
                table: "Arrangements",
                column: "PopulationId");

            migrationBuilder.CreateIndex(
                name: "IX_Square_ArrangementId",
                table: "Square",
                column: "ArrangementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Square");

            migrationBuilder.DropTable(
                name: "Arrangements");

            migrationBuilder.DropTable(
                name: "Population");
        }
    }
}
