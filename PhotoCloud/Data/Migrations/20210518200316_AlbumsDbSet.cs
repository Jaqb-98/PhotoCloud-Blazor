using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoCloud.Data.Migrations
{
    public partial class AlbumsDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumModel_AspNetUsers_ApplicationUserId",
                table: "AlbumModel");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumModelPhotoModel_AlbumModel_AlbumsId",
                table: "AlbumModelPhotoModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlbumModel",
                table: "AlbumModel");

            migrationBuilder.RenameTable(
                name: "AlbumModel",
                newName: "Albums");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumModel_ApplicationUserId",
                table: "Albums",
                newName: "IX_Albums_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Albums",
                table: "Albums",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumModelPhotoModel_Albums_AlbumsId",
                table: "AlbumModelPhotoModel",
                column: "AlbumsId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_AspNetUsers_ApplicationUserId",
                table: "Albums",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumModelPhotoModel_Albums_AlbumsId",
                table: "AlbumModelPhotoModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_AspNetUsers_ApplicationUserId",
                table: "Albums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Albums",
                table: "Albums");

            migrationBuilder.RenameTable(
                name: "Albums",
                newName: "AlbumModel");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_ApplicationUserId",
                table: "AlbumModel",
                newName: "IX_AlbumModel_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlbumModel",
                table: "AlbumModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumModel_AspNetUsers_ApplicationUserId",
                table: "AlbumModel",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumModelPhotoModel_AlbumModel_AlbumsId",
                table: "AlbumModelPhotoModel",
                column: "AlbumsId",
                principalTable: "AlbumModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
