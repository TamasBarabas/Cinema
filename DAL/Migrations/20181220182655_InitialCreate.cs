using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Genre = table.Column<int>(nullable: false),
                    YearOfRelease = table.Column<int>(nullable: false),
                    RunningTimeInMinutes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_Ratings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Genre", "RunningTimeInMinutes", "Title", "YearOfRelease" },
                values: new object[,]
                {
                    { 1, 0, 142, "The Shawshank Redemption", 1994 },
                    { 9, 4, 148, "The Good, the Bad and the Ugly", 1966 },
                    { 8, 0, 154, "Pulp Fiction", 1994 },
                    { 7, 3, 201, "The Lord of the Rings: The Return of the King", 2003 },
                    { 6, 0, 195, "Schindler's List", 1993 },
                    { 10, 2, 139, "Fight Club", 1999 },
                    { 4, 2, 152, "The Dark Knight", 2008 },
                    { 3, 1, 202, "The Godfather: Part II", 1974 },
                    { 2, 1, 175, "The Godfather", 1972 },
                    { 5, 0, 96, "12 Angry Men", 1957 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name" },
                values: new object[,]
                {
                    { 9, "User9" },
                    { 1, "User1" },
                    { 2, "User2" },
                    { 3, "User3" },
                    { 4, "User4" },
                    { 5, "User5" },
                    { 6, "User6" },
                    { 7, "User7" },
                    { 8, "User8" },
                    { 10, "User10" }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "UserId", "MovieId", "Value" },
                values: new object[,]
                {
                    { 1, 1, 5 },
                    { 8, 6, 5 },
                    { 8, 5, 5 },
                    { 7, 6, 5 },
                    { 7, 5, 3 },
                    { 7, 2, 5 },
                    { 6, 6, 5 },
                    { 6, 5, 4 },
                    { 6, 2, 4 },
                    { 5, 9, 4 },
                    { 5, 8, 4 },
                    { 5, 7, 4 },
                    { 5, 6, 5 },
                    { 5, 5, 4 },
                    { 5, 2, 3 },
                    { 4, 9, 2 },
                    { 4, 8, 5 },
                    { 4, 7, 5 },
                    { 2, 2, 5 },
                    { 2, 3, 5 },
                    { 2, 4, 5 },
                    { 2, 5, 5 },
                    { 2, 6, 5 },
                    { 2, 7, 5 },
                    { 9, 5, 5 },
                    { 3, 2, 4 },
                    { 3, 5, 4 },
                    { 3, 6, 5 },
                    { 3, 7, 5 },
                    { 4, 2, 3 },
                    { 4, 5, 4 },
                    { 4, 6, 5 },
                    { 3, 4, 1 },
                    { 9, 6, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieId",
                table: "Ratings",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
