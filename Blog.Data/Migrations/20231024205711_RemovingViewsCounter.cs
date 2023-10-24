using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovingViewsCounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersArticlesViews");

            migrationBuilder.DropTable(
                name: "UsersReviewsViews");

            migrationBuilder.DropTable(
                name: "UsersVideosViews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersArticlesViews",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArticleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersArticlesViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersArticlesViews_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersArticlesViews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersReviewsViews",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReviewId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersReviewsViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersReviewsViews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersReviewsViews_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersVideosViews",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersVideosViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersVideosViews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersVideosViews_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersArticlesViews_ArticleId",
                table: "UsersArticlesViews",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersArticlesViews_UserId",
                table: "UsersArticlesViews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersReviewsViews_ReviewId",
                table: "UsersReviewsViews",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersReviewsViews_UserId",
                table: "UsersReviewsViews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersVideosViews_UserId",
                table: "UsersVideosViews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersVideosViews_VideoId",
                table: "UsersVideosViews",
                column: "VideoId");
        }
    }
}
