using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiaryOwner",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaryOwner",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");
        }
    }
}
