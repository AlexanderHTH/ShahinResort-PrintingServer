using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Correct_PrinterErrorLog_TB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrintedId",
                table: "PrinterErrorLog_TB",
                newName: "PrinterId");

            migrationBuilder.RenameIndex(
                name: "IX_PrinterErrorLog_TB_PrintedId",
                table: "PrinterErrorLog_TB",
                newName: "IX_PrinterErrorLog_TB_PrinterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrinterId",
                table: "PrinterErrorLog_TB",
                newName: "PrintedId");

            migrationBuilder.RenameIndex(
                name: "IX_PrinterErrorLog_TB_PrinterId",
                table: "PrinterErrorLog_TB",
                newName: "IX_PrinterErrorLog_TB_PrintedId");
        }
    }
}
