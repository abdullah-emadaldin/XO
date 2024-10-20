using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XO.EF.Migrations
{
    /// <inheritdoc />
    public partial class secondedit6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserConnections",
                table: "UserConnections");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserConnections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserConnections",
                table: "UserConnections",
                column: "ConnectionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConnections_UserId",
                table: "UserConnections",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserConnections",
                table: "UserConnections");

            migrationBuilder.DropIndex(
                name: "IX_UserConnections_UserId",
                table: "UserConnections");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserConnections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserConnections",
                table: "UserConnections",
                columns: new[] { "UserId", "ConnectionId" });
        }
    }
}
