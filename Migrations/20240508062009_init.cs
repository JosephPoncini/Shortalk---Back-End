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
                name: "LobbyInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LobbyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamMemberA1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberA2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberA3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberA4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberA5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamMemberB5 = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "LobbyInfo");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
