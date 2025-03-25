﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingServer.Infrastructure.Migrations.User_DB
{
    /// <inheritdoc />
    public partial class UpdateUpUser001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TokenVersion",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenVersion",
                table: "AspNetUsers");
        }
    }
}
