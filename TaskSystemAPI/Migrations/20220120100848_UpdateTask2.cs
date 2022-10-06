using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListAPI.Migrations
{
    public partial class UpdateTask2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResponsibleUserId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ResponsibleUserId",
                table: "Tasks",
                column: "ResponsibleUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_ResponsibleUserId",
                table: "Tasks",
                column: "ResponsibleUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_ResponsibleUserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ResponsibleUserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserId",
                table: "Tasks");
        }
    }
}
