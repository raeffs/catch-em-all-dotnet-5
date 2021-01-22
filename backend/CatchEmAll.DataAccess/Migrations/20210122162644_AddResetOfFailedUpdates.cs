using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CatchEmAll.Migrations
{
  public partial class AddResetOfFailedUpdates : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "Update_LastAttempted",
          table: "SearchQueries",
          type: "datetimeoffset",
          nullable: false,
          defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

      migrationBuilder.AddColumn<int>(
          name: "Update_NumberOfResets",
          table: "SearchQueries",
          type: "int",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "Update_LastAttempted",
          table: "Auctions",
          type: "datetimeoffset",
          nullable: false,
          defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

      migrationBuilder.AddColumn<int>(
          name: "Update_NumberOfResets",
          table: "Auctions",
          type: "int",
          nullable: false,
          defaultValue: 0);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "Update_LastAttempted",
          table: "SearchQueries");

      migrationBuilder.DropColumn(
          name: "Update_NumberOfResets",
          table: "SearchQueries");

      migrationBuilder.DropColumn(
          name: "Update_LastAttempted",
          table: "Auctions");

      migrationBuilder.DropColumn(
          name: "Update_NumberOfResets",
          table: "Auctions");
    }
  }
}
