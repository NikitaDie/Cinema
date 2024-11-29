using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Storage.Migrations
{
    /// <inheritdoc />
    public partial class OnDeleteCascadeWasAdded : Migration
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
    }
}
