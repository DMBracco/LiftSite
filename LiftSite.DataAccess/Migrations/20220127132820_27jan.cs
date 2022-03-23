using Microsoft.EntityFrameworkCore.Migrations;

namespace LiftSite.DataAccess.Migrations
{
    public partial class _27jan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Equipments");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Equipments",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Equipments");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Equipments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
