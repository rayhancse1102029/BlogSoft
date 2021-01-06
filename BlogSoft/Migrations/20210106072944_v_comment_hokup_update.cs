using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogSoft.Migrations
{
    public partial class v_comment_hokup_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_BlogPosts_blogPostId",
                table: "Comments");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_BlogPosts_blogPostId",
                table: "Comments",
                column: "blogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
