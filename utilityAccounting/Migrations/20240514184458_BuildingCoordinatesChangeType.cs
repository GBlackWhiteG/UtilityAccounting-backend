using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace utilityAccounting.Migrations
{
    /// <inheritdoc />
    public partial class BuildingCoordinatesChangeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double[]>(
                name: "Coordinates",
                table: "Buildings",
                type: "double precision[]",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Coordinates",
                table: "Buildings",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double[]),
                oldType: "double precision[]");
        }
    }
}
