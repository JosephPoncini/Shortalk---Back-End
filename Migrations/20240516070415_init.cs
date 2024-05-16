using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shortalk___Back_End.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LobbyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfRounds = table.Column<int>(type: "int", nullable: false),
                    TimeLimit = table.Column<int>(type: "int", nullable: false),
                    TeamMemberA1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberA2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberA3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberA4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberA5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Turn = table.Column<int>(type: "int", nullable: false),
                    Speaker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnePointWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThreePointWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Team1Score = table.Column<int>(type: "int", nullable: false),
                    Team2Score = table.Column<int>(type: "int", nullable: false),
                    OnePointWordHasBeenSaid = table.Column<bool>(type: "bit", nullable: false),
                    ThreePointWordHasBeenSaid = table.Column<bool>(type: "bit", nullable: false),
                    BuzzWords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkippedWords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnePointWords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThreePointWords = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LobbyInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LobbyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfRounds = table.Column<int>(type: "int", nullable: false),
                    TimeLimit = table.Column<int>(type: "int", nullable: false),
                    TeamMemberA1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberA2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberA3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberA4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberA5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadyStatusA1 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusA2 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusA3 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusA4 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusA5 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusB1 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusB2 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusB3 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusB4 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusB5 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LobbyInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameInfo");

            migrationBuilder.DropTable(
                name: "LobbyInfo");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
