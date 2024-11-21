using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace game_room_service.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToGameRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GameRooms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "GameRooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "GameRooms",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "GameRooms");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GameRooms");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "GameRooms");
        }
    }
}
