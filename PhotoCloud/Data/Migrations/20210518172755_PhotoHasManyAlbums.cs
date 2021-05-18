using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoCloud.Data.Migrations
{
    public partial class PhotoHasManyAlbums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AlbumModel_AlbumModelId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_AlbumModelId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "AlbumModelId",
                table: "Photos");

            migrationBuilder.CreateTable(
                name: "AlbumModelPhotoModel",
                columns: table => new
                {
                    AlbumsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhotosId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumModelPhotoModel", x => new { x.AlbumsId, x.PhotosId });
                    table.ForeignKey(
                        name: "FK_AlbumModelPhotoModel_AlbumModel_AlbumsId",
                        column: x => x.AlbumsId,
                        principalTable: "AlbumModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumModelPhotoModel_Photos_PhotosId",
                        column: x => x.PhotosId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumModelPhotoModel_PhotosId",
                table: "AlbumModelPhotoModel",
                column: "PhotosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumModelPhotoModel");

            migrationBuilder.AddColumn<string>(
                name: "AlbumModelId",
                table: "Photos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AlbumModelId",
                table: "Photos",
                column: "AlbumModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AlbumModel_AlbumModelId",
                table: "Photos",
                column: "AlbumModelId",
                principalTable: "AlbumModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
