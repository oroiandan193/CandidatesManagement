using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatesManagement.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobCandidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IntervalStart = table.Column<TimeOnly>(type: "time", nullable: true),
                    IntervalEnd = table.Column<TimeOnly>(type: "time", nullable: true),
                    LinkedInProfile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GithubProfile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCandidates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidates_EmailAddress",
                table: "JobCandidates",
                column: "EmailAddress",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobCandidates");
        }
    }
}
