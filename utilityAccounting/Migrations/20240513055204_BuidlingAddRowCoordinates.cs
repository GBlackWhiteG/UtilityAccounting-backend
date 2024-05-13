using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace utilityAccounting.Migrations
{
    /// <inheritdoc />
    public partial class BuidlingAddRowCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Coordinates",
                table: "Buildings",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "Buildings");
        }
    }
}
