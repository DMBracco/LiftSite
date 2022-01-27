using Microsoft.EntityFrameworkCore.Migrations;

namespace LiftSite.DataAccess.Migrations
{
    public partial class New2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Images",
                newName: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Images",
                newName: "Deleted");
        }
    }
}
