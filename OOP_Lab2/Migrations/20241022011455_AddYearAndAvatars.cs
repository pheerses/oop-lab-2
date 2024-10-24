using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOP_Lab2.Migrations
{
    /// <inheritdoc />
    public partial class AddYearAndAvatars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarPath",
                table: "Artists",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Albums",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarPath",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Albums");
        }
    }
}
