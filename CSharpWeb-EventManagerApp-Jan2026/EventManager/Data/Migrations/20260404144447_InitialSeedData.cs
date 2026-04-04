using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Technology" },
                    { 2, "Business" },
                    { 3, "Education" },
                    { 4, "Health" },
                    { 5, "Science" },
                    { 6, "Art" },
                    { 7, "Marketing" },
                    { 8, "Finance" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CategoryId", "Description", "EndDate", "MaxParticipants", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, 1, "A conference covering the fundamentals of ASP.NET Core MVC.", new DateTime(2026, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 300, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP.NET Core Fundamentals Conference" },
                    { 2, 1, "Topics: MVC, REST, validation, and security basics for web apps.", new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 400, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Modern Web Development Conference" },
                    { 3, 2, "Hands-on workshop focused on model binding and validation.", new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Model Binding and Validation Workshop" },
                    { 4, 2, "Build forms with tag helpers and display validation messages properly.", new DateTime(2026, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 35, new DateTime(2026, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Razor Forms Workshop" },
                    { 5, 3, "How to keep controller actions small and predictable with ModelState.", new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 120, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clean Controllers Seminar" },
                    { 6, 3, "Server-side validation patterns and common mistakes in MVC apps.", new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 120, new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Validation Best Practices Seminar" },
                    { 7, 4, "DbContext, migrations, relationships, and seeding essentials.", new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 80, new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "EF Core Essentials Training" },
                    { 8, 4, "Practice form submissions, invalid model states, and error rendering.", new DateTime(2026, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 70, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Testing MVC Forms Training" },
                    { 9, 5, "Community meetup: mini talks and networking for .NET developers.", new DateTime(2026, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 150, new DateTime(2026, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sofia .NET Meetup" },
                    { 10, 5, "Students present their projects and discuss common issues and fixes.", new DateTime(2026, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 150, new DateTime(2026, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Student Projects Meetup" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
