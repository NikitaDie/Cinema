using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Storage.Migrations
{
    /// <inheritdoc />
    public partial class NameByActorWasSplittedIntoFirsLastName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "actors",
                newName: "last_name");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "actors",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                table: "actors");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "actors",
                newName: "name");
        }
    }
}
