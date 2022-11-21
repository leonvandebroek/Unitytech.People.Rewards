using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unitytech.People.Rewards.Server.Migrations
{
    public partial class InitialSQLServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zipcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemberSince = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemberUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwardDocument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntervalInMonths = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeopleRewards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RewardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeopleRewards_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeopleRewards_Rewards_RewardId",
                        column: x => x.RewardId,
                        principalTable: "Rewards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BirthDate", "City", "DateCreated", "DateDeleted", "Department", "Email", "FirstName", "Initials", "LastName", "MemberNumber", "MemberSince", "MemberUntil", "MiddleName", "Phone", "Street", "StreetNumber", "Zipcode" },
                values: new object[] { new Guid("a786916e-d36f-4612-b679-77a1c8986052"), new DateTime(1994, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Duckstad", new DateTime(2022, 8, 7, 15, 18, 0, 972, DateTimeKind.Local).AddTicks(1880), new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), "Roverscouts", "tvdt@me.com", "Test", "T.E.S.T.", "Test", "1258258", new DateTime(1999, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "van der", "06-87654321", "Ganzenlaan", "21", "7544HO" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BirthDate", "City", "DateCreated", "DateDeleted", "Department", "Email", "FirstName", "Initials", "LastName", "MemberNumber", "MemberSince", "MemberUntil", "MiddleName", "Phone", "Street", "StreetNumber", "Zipcode" },
                values: new object[] { new Guid("ace7cad3-cea8-4520-814e-f14c89778908"), new DateTime(1991, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simcity", new DateTime(2022, 8, 7, 15, 18, 0, 972, DateTimeKind.Local).AddTicks(1910), new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), "Plusscouts", "pp@me.com", "Pietje", "P.P", "Puk", "1258259", new DateTime(1996, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "06-87651234", "Motherload avenue", "5", "1234DH" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BirthDate", "City", "DateCreated", "DateDeleted", "Department", "Email", "FirstName", "Initials", "LastName", "MemberNumber", "MemberSince", "MemberUntil", "MiddleName", "Phone", "Street", "StreetNumber", "Zipcode" },
                values: new object[] { new Guid("c35bee56-f515-4158-955c-04bb8b15c007"), new DateTime(1992, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teststad", new DateTime(2022, 8, 7, 15, 18, 0, 972, DateTimeKind.Local).AddTicks(1800), new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), "Scouts", "lvdh@me.com", "Léon", "L.G.E.", "Broek", "1258257", new DateTime(1997, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "van de", "06-12345678", "Teststraat", "1", "7546HR" });

            migrationBuilder.CreateIndex(
                name: "IX_PeopleRewards_PersonId",
                table: "PeopleRewards",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleRewards_RewardId",
                table: "PeopleRewards",
                column: "RewardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeopleRewards");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Rewards");
        }
    }
}
