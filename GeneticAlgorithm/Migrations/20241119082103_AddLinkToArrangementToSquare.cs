using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenAlgo.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkToArrangementToSquare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Square_Arrangements_ArrangementId",
                table: "Square");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Square",
                table: "Square");

            migrationBuilder.RenameTable(
                name: "Square",
                newName: "Sqaures");

            migrationBuilder.RenameIndex(
                name: "IX_Square_ArrangementId",
                table: "Sqaures",
                newName: "IX_Sqaures_ArrangementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sqaures",
                table: "Sqaures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sqaures_Arrangements_ArrangementId",
                table: "Sqaures",
                column: "ArrangementId",
                principalTable: "Arrangements",
                principalColumn: "ArrangementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sqaures_Arrangements_ArrangementId",
                table: "Sqaures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sqaures",
                table: "Sqaures");

            migrationBuilder.RenameTable(
                name: "Sqaures",
                newName: "Square");

            migrationBuilder.RenameIndex(
                name: "IX_Sqaures_ArrangementId",
                table: "Square",
                newName: "IX_Square_ArrangementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Square",
                table: "Square",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Square_Arrangements_ArrangementId",
                table: "Square",
                column: "ArrangementId",
                principalTable: "Arrangements",
                principalColumn: "ArrangementId");
        }
    }
}
