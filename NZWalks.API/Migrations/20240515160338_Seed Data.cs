using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1d3c98ae-6c68-44b3-b1af-f0384e899f18"), "Easy" },
                    { new Guid("4a659fbe-43d0-4edd-b88a-6c74bd3ae55d"), "Medium" },
                    { new Guid("ee171b45-96a1-4d96-babb-d14c273c6d04"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2be60b15-a0ad-48d9-96b1-67482b429070"), "BOP", "Bay Of Plenty", "image3.jpg" },
                    { new Guid("523ee4a1-2547-4fba-8de8-7e96585223d9"), "AKL", "Auckland", "image.jpg" },
                    { new Guid("53e7a6d5-2065-4977-8784-e383cffdce36"), "NTL", "Northland", "image2.jpg" },
                    { new Guid("63012100-48c1-4cbb-9e5b-b0fdeffca151"), "STL", "Southland", null },
                    { new Guid("bab3cb4c-677e-4ca5-a1b8-a7aa359c1a0a"), "WGN", "Nelson", "image5.jpg" },
                    { new Guid("e56c6ae9-fe62-4b6b-b96d-aed3b428ce62"), "WGN", "Wellington", "image4.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("1d3c98ae-6c68-44b3-b1af-f0384e899f18"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("4a659fbe-43d0-4edd-b88a-6c74bd3ae55d"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("ee171b45-96a1-4d96-babb-d14c273c6d04"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2be60b15-a0ad-48d9-96b1-67482b429070"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("523ee4a1-2547-4fba-8de8-7e96585223d9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("53e7a6d5-2065-4977-8784-e383cffdce36"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("63012100-48c1-4cbb-9e5b-b0fdeffca151"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("bab3cb4c-677e-4ca5-a1b8-a7aa359c1a0a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e56c6ae9-fe62-4b6b-b96d-aed3b428ce62"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
