using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzyAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuestionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectOptionIndex",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Questions",
                newName: "Question");

            migrationBuilder.RenameColumn(
                name: "Options",
                table: "Questions",
                newName: "IncorrectAnswers");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "Question",
                table: "Questions",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "IncorrectAnswers",
                table: "Questions",
                newName: "Options");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Questions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "CorrectOptionIndex",
                table: "Questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
