using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Data.Migrations
{
    /// <inheritdoc />
    public partial class SyncHotelFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "hotels");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "rooms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "hotel_name",
                table: "hotels",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "AmadeusHotelId",
                table: "hotels",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "hotels",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "hotels",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CityCode",
                table: "hotels",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsLiveApiSourced",
                table: "hotels",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerNight",
                table: "hotels",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "AmadeusHotelId",
                table: "hotels");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "hotels");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "hotels");

            migrationBuilder.DropColumn(
                name: "CityCode",
                table: "hotels");

            migrationBuilder.DropColumn(
                name: "IsLiveApiSourced",
                table: "hotels");

            migrationBuilder.DropColumn(
                name: "PricePerNight",
                table: "hotels");

            migrationBuilder.AlterColumn<string>(
                name: "hotel_name",
                table: "hotels",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "hotels",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
