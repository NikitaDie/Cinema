using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Storage.Migrations
{
    /// <inheritdoc />
    public partial class SoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_branch",
                table: "auditoriums");

            migrationBuilder.DropForeignKey(
                name: "fk_session",
                table: "pricelist");

            migrationBuilder.DropForeignKey(
                name: "fk_status",
                table: "pricelist");

            migrationBuilder.DropForeignKey(
                name: "fk_auditorium",
                table: "seats");

            migrationBuilder.DropForeignKey(
                name: "fk_status",
                table: "seats");

            migrationBuilder.DropForeignKey(
                name: "fk_auditorium",
                table: "sessions");

            migrationBuilder.DropForeignKey(
                name: "fk_movie",
                table: "sessions");

            migrationBuilder.DropForeignKey(
                name: "fk_client",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "fk_seat",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "fk_session",
                table: "tickets");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "statuses",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "seats",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "branches",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "auditoriums",
                newName: "DeletedAt");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "tickets",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "tickets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "statuses",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "statuses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "sessions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "sessions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "seats",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "seats",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "pricelist",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "pricelist",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "movies",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "movies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "genres",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "genres",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "clients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "clients",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "branches",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "branches",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "auditoriums",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "auditoriums",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "actors",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "actors",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "fk_branch",
                table: "auditoriums",
                column: "branch_id",
                principalTable: "branches",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_session",
                table: "pricelist",
                column: "session_id",
                principalTable: "sessions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_status",
                table: "pricelist",
                column: "status_id",
                principalTable: "statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_auditorium",
                table: "seats",
                column: "auditorium_id",
                principalTable: "auditoriums",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_status",
                table: "seats",
                column: "status_id",
                principalTable: "statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_auditorium",
                table: "sessions",
                column: "auditorium_id",
                principalTable: "auditoriums",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_movie",
                table: "sessions",
                column: "movie_id",
                principalTable: "movies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_client",
                table: "tickets",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_seat",
                table: "tickets",
                column: "seat_id",
                principalTable: "seats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_session",
                table: "tickets",
                column: "session_id",
                principalTable: "sessions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_branch",
                table: "auditoriums");

            migrationBuilder.DropForeignKey(
                name: "fk_session",
                table: "pricelist");

            migrationBuilder.DropForeignKey(
                name: "fk_status",
                table: "pricelist");

            migrationBuilder.DropForeignKey(
                name: "fk_auditorium",
                table: "seats");

            migrationBuilder.DropForeignKey(
                name: "fk_status",
                table: "seats");

            migrationBuilder.DropForeignKey(
                name: "fk_auditorium",
                table: "sessions");

            migrationBuilder.DropForeignKey(
                name: "fk_movie",
                table: "sessions");

            migrationBuilder.DropForeignKey(
                name: "fk_client",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "fk_seat",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "fk_session",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "statuses");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "seats");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "pricelist");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "pricelist");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "genres");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "genres");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "branches");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "auditoriums");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "actors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "actors");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "statuses",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "seats",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "branches",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "auditoriums",
                newName: "deleted_at");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "deleted_at",
                table: "statuses",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "deleted_at",
                table: "seats",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "branches",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "deleted_at",
                table: "auditoriums",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_branch",
                table: "auditoriums",
                column: "branch_id",
                principalTable: "branches",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_session",
                table: "pricelist",
                column: "session_id",
                principalTable: "sessions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_status",
                table: "pricelist",
                column: "status_id",
                principalTable: "statuses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_auditorium",
                table: "seats",
                column: "auditorium_id",
                principalTable: "auditoriums",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_status",
                table: "seats",
                column: "status_id",
                principalTable: "statuses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_auditorium",
                table: "sessions",
                column: "auditorium_id",
                principalTable: "auditoriums",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_movie",
                table: "sessions",
                column: "movie_id",
                principalTable: "movies",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_client",
                table: "tickets",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_seat",
                table: "tickets",
                column: "seat_id",
                principalTable: "seats",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_session",
                table: "tickets",
                column: "session_id",
                principalTable: "sessions",
                principalColumn: "id");
        }
    }
}
