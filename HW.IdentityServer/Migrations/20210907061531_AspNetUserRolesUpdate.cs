using Microsoft.EntityFrameworkCore.Migrations;

namespace HW.IdentityServer.Migrations
{
    public partial class AspNetUserRolesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerType",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "SellerType",
                table: "AspNetUserRoles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerType",
                table: "AspNetUserRoles");

            migrationBuilder.AddColumn<string>(
                name: "SellerType",
                table: "AspNetRoles",
                nullable: true);
        }
    }
}
