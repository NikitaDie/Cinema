using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Storage.Migrations
{
    /// <inheritdoc />
    public partial class UniqueConstrainsOnMovieActorMovieGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_MovieId_GenreId",
                table: "MovieGenre",
                columns: new[] { "MovieId", "GenreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieActor_MovieId_ActorId",
                table: "MovieActor",
                columns: new[] { "MovieId", "ActorId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MovieGenre_MovieId_GenreId",
                table: "MovieGenre");

            migrationBuilder.DropIndex(
                name: "IX_MovieActor_MovieId_ActorId",
                table: "MovieActor");
        }
    }
}
