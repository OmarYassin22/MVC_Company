using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.DataAccess.Contexts.Migrations
{
    public partial class AddIAccept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IAccept",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IAccept",
                table: "AspNetUsers");
        }
    }
}
