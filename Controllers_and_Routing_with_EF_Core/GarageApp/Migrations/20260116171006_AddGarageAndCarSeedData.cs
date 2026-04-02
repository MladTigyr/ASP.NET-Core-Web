using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarageApp.Migrations
{
    /// <inheritdoc />
    public partial class AddGarageAndCarSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Garages_GarageId",
                table: "Cars");

            migrationBuilder.InsertData(
                table: "Garages",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "Sofia", "Downtown Garage" },
                    { 2, "Plovdiv", "Uptown Garage" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "GarageId", "IsAvailable", "Make", "Model", "Type", "Year" },
                values: new object[,]
                {
                    { 1, 1, true, "Audi", "Rs3", 0, 2020 },
                    { 2, 1, true, "BMW", "M5", 2, 2019 },
                    { 3, 2, false, "Mercedes", "C63", 1, 2018 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Garages_GarageId",
                table: "Cars",
                column: "GarageId",
                principalTable: "Garages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Garages_GarageId",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Garages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Garages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Garages_GarageId",
                table: "Cars",
                column: "GarageId",
                principalTable: "Garages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
