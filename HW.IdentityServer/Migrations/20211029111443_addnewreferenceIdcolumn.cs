using Microsoft.EntityFrameworkCore.Migrations;

namespace HW.IdentityServer.Migrations
{
    public partial class addnewreferenceIdcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerType",
                table: "AspNetUserRoles");

            migrationBuilder.AddColumn<long>(
                name: "ReferenceId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "SellerType",
                table: "AspNetUserRoles",
                nullable: true);
        }
    }
}
