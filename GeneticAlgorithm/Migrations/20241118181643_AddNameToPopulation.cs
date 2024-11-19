using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenAlgo.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToPopulation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Populations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Populations");
        }
    }
}
