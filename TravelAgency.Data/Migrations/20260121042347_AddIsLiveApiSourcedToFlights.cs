using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsLiveApiSourcedToFlights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "flights",
                type: "string",
                nullable: true,
                defaultValue: false);

        }
    }
}
