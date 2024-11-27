using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Storage.Migrations
{
    /// <inheritdoc />
    public partial class DeletedArWasAddedToSeatAuditoriumStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "deleted_at",
                table: "statuses",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "deleted_at",
                table: "seats",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "deleted_at",
                table: "auditoriums",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "statuses");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "seats");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "auditoriums");
        }
    }
}
