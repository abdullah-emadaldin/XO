using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XO.EF.Migrations
{
    /// <inheritdoc />
    public partial class secondedit7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPlaying",
                table: "UserConnections",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlaying",
                table: "UserConnections");
        }
    }
}
