using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xronopic.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserIdTypeToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timelines_User_UserId",
                table: "Timelines");

            migrationBuilder.DropIndex(
                name: "IX_Timelines_UserId",
                table: "Timelines");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Timelines",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Timelines",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timelines_UserId1",
                table: "Timelines",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Timelines_User_UserId1",
                table: "Timelines",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timelines_User_UserId1",
                table: "Timelines");

            migrationBuilder.DropIndex(
                name: "IX_Timelines_UserId1",
                table: "Timelines");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Timelines");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Timelines",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Timelines_UserId",
                table: "Timelines",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timelines_User_UserId",
                table: "Timelines",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
