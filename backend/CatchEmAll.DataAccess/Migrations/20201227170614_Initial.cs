using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatchEmAll.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Info_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Info_Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Info_Ends = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Info_IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    Info_IsSold = table.Column<bool>(type: "bit", nullable: false),
                    Price_BidPrice = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Price_PurchasePrice = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Price_FinalPrice = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Update_Updated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Update_IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    Update_NumberOfFailures = table.Column<int>(type: "int", nullable: false),
                    Provider_Key = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Provider_Value = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Settings_EmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchQueries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Criteria_WithAllTheseWords = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Criteria_WithOneOfTheseWords = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Criteria_WithExactlyTheseWords = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Criteria_WithNoneOfTheseWords = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Update_Updated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Update_IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    Update_NumberOfFailures = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchQueries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchQueries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SearchResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QueryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lifetime_Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Lifetime_Deleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Lifetime_IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchResults_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SearchResults_SearchQueries_QueryId",
                        column: x => x.QueryId,
                        principalTable: "SearchQueries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_Provider_Key_Provider_Value",
                table: "Auctions",
                columns: new[] { "Provider_Key", "Provider_Value" });

            migrationBuilder.CreateIndex(
                name: "IX_SearchQueries_UserId",
                table: "SearchQueries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchResults_AuctionId",
                table: "SearchResults",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchResults_QueryId_AuctionId",
                table: "SearchResults",
                columns: new[] { "QueryId", "AuctionId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchResults");

            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "SearchQueries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
