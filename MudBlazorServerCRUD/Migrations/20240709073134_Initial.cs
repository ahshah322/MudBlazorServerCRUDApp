using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MudBlazorServerCRUD.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameB = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradeId);
                    table.ForeignKey(
                        name: "FK_Grades_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name", "NameA", "NameB" },
                values: new object[,]
                {
                    { 1, "Male", "Man", "M" },
                    { 2, "Female", "Woman", "W" },
                    { 3, "Other", "Not Mentioned", "O" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateOfBirth", "Email", "GenderId", "Marks", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1996, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ali@gmail.com", 1, 0, "ali@gmail.com" },
                    { 2, new DateTime(2000, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "shabi@gmail.com", 1, 0, "shabi" },
                    { 3, new DateTime(1998, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "zillay@gmail.com", 1, 0, "zillay" },
                    { 4, new DateTime(1993, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "zulqar@gmail.com", 1, 0, "zulqar" },
                    { 5, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "elisa@gmail.com", 2, 0, "elisa" },
                    { 6, new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mehrub@gmail.com", 3, 0, "mehrub" }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "GradeId", "Score", "StudentId", "Subject" },
                values: new object[,]
                {
                    { 1, 90, 1, "Math" },
                    { 2, 85, 1, "Science" },
                    { 3, 88, 2, "Math" },
                    { 4, 92, 2, "Science" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GenderId",
                table: "Students",
                column: "GenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Genders");
        }
    }
}
