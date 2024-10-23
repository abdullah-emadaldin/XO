using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XO.EF.Migrations
{
    /// <inheritdoc />
    public partial class commentiswaiting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWaiting",
                table: "UserConnections");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWaiting",
                table: "UserConnections",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
