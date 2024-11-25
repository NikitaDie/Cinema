using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Storage.Migrations
{
    /// <inheritdoc />
    public partial class BrancheModelWasChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "branches",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "branches",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "branches",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "branches",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "region",
                table: "branches",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "zip_code",
                table: "branches",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "branches");

            migrationBuilder.DropColumn(
                name: "city",
                table: "branches");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "branches");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "branches");

            migrationBuilder.DropColumn(
                name: "region",
                table: "branches");

            migrationBuilder.DropColumn(
                name: "zip_code",
                table: "branches");
        }
    }
}
