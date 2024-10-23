using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XO.EF.Migrations
{
    /// <inheritdoc />
    public partial class secondedit5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserConnections_Users_UserId",
                table: "UserConnections");

            migrationBuilder.AddForeignKey(
                name: "FK_UserConnections_Users_UserId",
                table: "UserConnections",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserConnections_Users_UserId",
                table: "UserConnections");

            migrationBuilder.AddForeignKey(
                name: "FK_UserConnections_Users_UserId",
                table: "UserConnections",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
