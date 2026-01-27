using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingColumnToFlights2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "flights",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "flights");
        }
    }
}
