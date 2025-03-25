using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IdentityEF_Handle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Report_TB",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "PrinterErrorLog_TB",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Printer_TB",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Printed_TB",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Country_TB",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "ClientReport_TB",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Client_TB",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Report_TB");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "PrinterErrorLog_TB");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Printer_TB");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Printed_TB");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Country_TB");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ClientReport_TB");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Client_TB");
        }
    }
}
