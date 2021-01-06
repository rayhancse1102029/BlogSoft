using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogSoft.Migrations
{
    public partial class v_comment_hokup_done : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_BlogPosts_blogPostId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "blogPostId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "applicationUserId",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "applicationUserId",
                table: "BlogPosts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_applicationUserId",
                table: "Comments",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_applicationUserId",
                table: "BlogPosts",
                column: "applicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_AspNetUsers_applicationUserId",
                table: "BlogPosts",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_applicationUserId",
                table: "Comments",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_BlogPosts_blogPostId",
                table: "Comments",
                column: "blogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_AspNetUsers_applicationUserId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_applicationUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_BlogPosts_blogPostId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_applicationUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_applicationUserId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "applicationUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "applicationUserId",
                table: "BlogPosts");

            migrationBuilder.AlterColumn<int>(
                name: "blogPostId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_BlogPosts_blogPostId",
                table: "Comments",
                column: "blogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
