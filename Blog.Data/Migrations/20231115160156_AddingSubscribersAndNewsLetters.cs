using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingSubscribersAndNewsLetters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsLettersSubscribers_NewsLetter_NewsLetterId",
                table: "NewsLettersSubscribers");

            migrationBuilder.DropForeignKey(
                name: "FK_ToolsNewsLetters_NewsLetter_NewsLetterId",
                table: "ToolsNewsLetters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsLetter",
                table: "NewsLetter");

            migrationBuilder.RenameTable(
                name: "NewsLetter",
                newName: "NewsLetters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsLetters",
                table: "NewsLetters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsLettersSubscribers_NewsLetters_NewsLetterId",
                table: "NewsLettersSubscribers",
                column: "NewsLetterId",
                principalTable: "NewsLetters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ToolsNewsLetters_NewsLetters_NewsLetterId",
                table: "ToolsNewsLetters",
                column: "NewsLetterId",
                principalTable: "NewsLetters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsLettersSubscribers_NewsLetters_NewsLetterId",
                table: "NewsLettersSubscribers");

            migrationBuilder.DropForeignKey(
                name: "FK_ToolsNewsLetters_NewsLetters_NewsLetterId",
                table: "ToolsNewsLetters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsLetters",
                table: "NewsLetters");

            migrationBuilder.RenameTable(
                name: "NewsLetters",
                newName: "NewsLetter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsLetter",
                table: "NewsLetter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsLettersSubscribers_NewsLetter_NewsLetterId",
                table: "NewsLettersSubscribers",
                column: "NewsLetterId",
                principalTable: "NewsLetter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ToolsNewsLetters_NewsLetter_NewsLetterId",
                table: "ToolsNewsLetters",
                column: "NewsLetterId",
                principalTable: "NewsLetter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
