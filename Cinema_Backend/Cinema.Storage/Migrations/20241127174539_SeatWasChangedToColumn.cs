using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Storage.Migrations
{
    /// <inheritdoc />
    public partial class SeatWasChangedToColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "seat",
                table: "seats",
                newName: "column");

            migrationBuilder.RenameIndex(
                name: "unique_row_seat",
                table: "seats",
                newName: "unique_row_column");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "column",
                table: "seats",
                newName: "seat");

            migrationBuilder.RenameIndex(
                name: "unique_row_column",
                table: "seats",
                newName: "unique_row_seat");
        }
    }
}
