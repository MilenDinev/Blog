using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewsLetterRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsLetter",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLetter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NormalizedTag = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToolsNewsLetters",
                columns: table => new
                {
                    ToolId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NewsLetterId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolsNewsLetters", x => new { x.ToolId, x.NewsLetterId });
                    table.ForeignKey(
                        name: "FK_ToolsNewsLetters_NewsLetter_NewsLetterId",
                        column: x => x.NewsLetterId,
                        principalTable: "NewsLetter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ToolsNewsLetters_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsLettersSubscribers",
                columns: table => new
                {
                    NewsLetterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubscriberId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLettersSubscribers", x => new { x.NewsLetterId, x.SubscriberId });
                    table.ForeignKey(
                        name: "FK_NewsLettersSubscribers_NewsLetter_NewsLetterId",
                        column: x => x.NewsLetterId,
                        principalTable: "NewsLetter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsLettersSubscribers_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsLettersSubscribers_SubscriberId",
                table: "NewsLettersSubscribers",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolsNewsLetters_NewsLetterId",
                table: "ToolsNewsLetters",
                column: "NewsLetterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsLettersSubscribers");

            migrationBuilder.DropTable(
                name: "ToolsNewsLetters");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "NewsLetter");
        }
    }
}
