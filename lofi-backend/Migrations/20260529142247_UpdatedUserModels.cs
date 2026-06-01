using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lofi_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_UserId",
                table: "Playlists");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Playlists",
                newName: "UserDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_UserId",
                table: "Playlists",
                newName: "IX_Playlists_UserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_UserDataId",
                table: "Playlists",
                column: "UserDataId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_UserDataId",
                table: "Playlists");

            migrationBuilder.RenameColumn(
                name: "UserDataId",
                table: "Playlists",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_UserDataId",
                table: "Playlists",
                newName: "IX_Playlists_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_UserId",
                table: "Playlists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
