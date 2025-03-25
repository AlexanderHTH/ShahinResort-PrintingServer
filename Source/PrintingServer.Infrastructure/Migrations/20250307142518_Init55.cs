using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init55 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Country_TB",
                newName: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Country_TB",
                newName: "Id");
        }
    }
}
