using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapisApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueBlogPostSlug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Question",
                table: "FAQ",
                newName: "QuestionEn");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "FAQ",
                newName: "QuestionAr");

            migrationBuilder.AddColumn<string>(
                name: "AnswerAr",
                table: "FAQ",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AnswerEn",
                table: "FAQ",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "BlogPost",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_Slug",
                table: "BlogPost",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlogPost_Slug",
                table: "BlogPost");

            migrationBuilder.DropColumn(
                name: "AnswerAr",
                table: "FAQ");

            migrationBuilder.DropColumn(
                name: "AnswerEn",
                table: "FAQ");

            migrationBuilder.RenameColumn(
                name: "QuestionEn",
                table: "FAQ",
                newName: "Question");

            migrationBuilder.RenameColumn(
                name: "QuestionAr",
                table: "FAQ",
                newName: "Answer");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "BlogPost",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
