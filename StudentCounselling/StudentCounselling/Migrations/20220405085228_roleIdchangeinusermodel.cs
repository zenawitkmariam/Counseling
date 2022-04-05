using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentCounselling.Migrations
{
    public partial class roleIdchangeinusermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "User",
                newName: "Role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "User",
                newName: "RoleId");
        }
    }
}
