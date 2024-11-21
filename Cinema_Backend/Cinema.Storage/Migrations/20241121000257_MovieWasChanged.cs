using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Storage.Migrations
{
    /// <inheritdoc />
    public partial class MovieWasChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "movies_assets_path_key",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "name",
                table: "movies");

            migrationBuilder.RenameColumn(
                name: "assets_path",
                table: "movies",
                newName: "starring");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "movies",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "age_rating",
                table: "movies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "director",
                table: "movies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "duration",
                table: "movies",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "genres",
                table: "movies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "image_path",
                table: "movies",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "inclusive_adaptation",
                table: "movies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "language",
                table: "movies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "original_title",
                table: "movies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "producer",
                table: "movies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "production_studio",
                table: "movies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "release_year",
                table: "movies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "rental_period_end",
                table: "movies",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "rental_period_start",
                table: "movies",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "screenplay",
                table: "movies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "movies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "trailer_link",
                table: "movies",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "movies_image_path_key",
                table: "movies",
                column: "image_path",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "movies_image_path_key",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "age_rating",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "director",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "duration",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "genres",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "image_path",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "inclusive_adaptation",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "language",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "original_title",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "producer",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "production_studio",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "release_year",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "rental_period_end",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "rental_period_start",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "screenplay",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "title",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "trailer_link",
                table: "movies");

            migrationBuilder.RenameColumn(
                name: "starring",
                table: "movies",
                newName: "assets_path");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "movies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "movies",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "movies_assets_path_key",
                table: "movies",
                column: "assets_path",
                unique: true);
        }
    }
}
