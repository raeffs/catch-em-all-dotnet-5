using Microsoft.EntityFrameworkCore.Migrations;

namespace CatchEmAll.Migrations
{
  public partial class AddSearchSettings : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.RenameColumn(
          name: "Priority",
          table: "SearchQueries",
          newName: "Settings_Priority");

      migrationBuilder.AddColumn<bool>(
          name: "Settings_AutoFilterDeletedDuplicates",
          table: "SearchQueries",
          type: "bit",
          nullable: false,
          defaultValue: false);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "Settings_AutoFilterDeletedDuplicates",
          table: "SearchQueries");

      migrationBuilder.RenameColumn(
          name: "Settings_Priority",
          table: "SearchQueries",
          newName: "Priority");
    }
  }
}
