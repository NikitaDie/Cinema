using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Storage.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "branches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("branches_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("clients_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    assets_path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("movies_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("statuses_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "auditoriums",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    branch_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("auditoriums_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_branch",
                        column: x => x.branch_id,
                        principalTable: "branches",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "seats",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    row = table.Column<short>(type: "smallint", nullable: false),
                    seat = table.Column<short>(type: "smallint", nullable: false),
                    x_position = table.Column<short>(type: "smallint", nullable: false),
                    y_position = table.Column<short>(type: "smallint", nullable: false),
                    auditorium_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("seats_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_auditorium",
                        column: x => x.auditorium_id,
                        principalTable: "auditoriums",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_status",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    start_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    auditorium_id = table.Column<Guid>(type: "uuid", nullable: false),
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sessions_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_auditorium",
                        column: x => x.auditorium_id,
                        principalTable: "auditoriums",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_movie",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "pricelist",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status_id = table.Column<Guid>(type: "uuid", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pricelist_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_session",
                        column: x => x.session_id,
                        principalTable: "sessions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_status",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    seat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("tickets_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_client",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_seat",
                        column: x => x.seat_id,
                        principalTable: "seats",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_session",
                        column: x => x.session_id,
                        principalTable: "sessions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_auditoriums_branch_id",
                table: "auditoriums",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "unique_id_branch",
                table: "auditoriums",
                columns: new[] { "id", "branch_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "unique_name_branch",
                table: "auditoriums",
                columns: new[] { "name", "branch_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "branches_name_key",
                table: "branches",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "clients_email_key",
                table: "clients",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "movies_assets_path_key",
                table: "movies",
                column: "assets_path",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pricelist_status_id",
                table: "pricelist",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "unique_session_status",
                table: "pricelist",
                columns: new[] { "session_id", "status_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_seats_auditorium_id",
                table: "seats",
                column: "auditorium_id");

            migrationBuilder.CreateIndex(
                name: "IX_seats_status_id",
                table: "seats",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "unique_row_seat",
                table: "seats",
                columns: new[] { "row", "seat", "auditorium_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "unique_x_position_y_position",
                table: "seats",
                columns: new[] { "x_position", "y_position", "auditorium_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sessions_auditorium_id",
                table: "sessions",
                column: "auditorium_id");

            migrationBuilder.CreateIndex(
                name: "IX_sessions_movie_id",
                table: "sessions",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "statuses_name_key",
                table: "statuses",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tickets_client_id",
                table: "tickets",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_seat_id",
                table: "tickets",
                column: "seat_id");

            migrationBuilder.CreateIndex(
                name: "unique_session_seat",
                table: "tickets",
                columns: new[] { "session_id", "seat_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pricelist");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "seats");

            migrationBuilder.DropTable(
                name: "sessions");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropTable(
                name: "auditoriums");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "branches");
        }
    }
}
