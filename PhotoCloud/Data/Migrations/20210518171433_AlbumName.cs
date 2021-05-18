using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoCloud.Data.Migrations
{
    public partial class AlbumName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlbumName",
                table: "AlbumModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumName",
                table: "AlbumModel");
        }
    }
}
