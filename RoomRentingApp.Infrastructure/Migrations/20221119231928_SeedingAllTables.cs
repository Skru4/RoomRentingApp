using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomRentingApp.Infrastructure.Migrations
{
    public partial class SeedingAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImageUrl", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3808cb45-d6fd-4604-9e32-f15574f56f8a", 0, "a3077d59-f4a4-4d37-960a-be1c1d7586a2", "renter@abv.bg", false, "Ivan", null, "Ivanov", false, null, "renter@abv.bg", "renter", "AQAAAAEAACcQAAAAEN0nMFDa8wQBYMVeKf89H2UXlvrDJX1NV5GaNpkXUcosp6UoE+X4besmPbagDCIqzQ==", null, false, "5a0f970b-2a2d-442f-b34e-7f5ae0060bf2", false, "renter" },
                    { "3ecf1600-5711-4b55-840a-9ba518a64005", 0, "e0869f43-c3da-4f00-a282-8035199857b3", "landlord@abv.bg", false, "Gosho", null, "Goshev", false, null, "landlord@abv.bg", "landlord", "AQAAAAEAACcQAAAAEFHBinYZhG9nZm4by42aeGs4JY4eq6LNexAfn1+D5FZTZI96ijJzA/badE0Xe85TwA==", null, false, "40a0103b-6cc1-4138-befc-d68fb4da4168", false, "landlord" }
                });

            migrationBuilder.InsertData(
                table: "RoomCategories",
                columns: new[] { "Id", "LandlordStatus", "RoomSize" },
                values: new object[,]
                {
                    { 1, "Live-out Landlord", "Big" },
                    { 2, "Live-in Landlord", "Small" },
                    { 3, "Live-out Landlord", "Large" }
                });

            migrationBuilder.InsertData(
                table: "Towns",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Minneapolis" },
                    { 2, "Worcester" },
                    { 3, "San Angelo" }
                });

            migrationBuilder.InsertData(
                table: "Landlords",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { new Guid("84354850-dc70-4ee8-826c-5ce9114baeb3"), "089999999", "3ecf1600-5711-4b55-840a-9ba518a64005" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Address", "Description", "ImageUrl", "LandlordId", "PricePerWeek", "RenterId", "RoomCategoryId", "TownId" },
                values: new object[] { new Guid("1dc7e29a-1d1a-4a4b-ae99-05c8f99922a5"), "920 Rocket Drive", "Luxurious attic room with double bed and big scenery window", "http://cdn.home-designing.com/wp-content/uploads/2016/08/rustic-attic-bedroom-wood-burning-fireplace.jpg", new Guid("84354850-dc70-4ee8-826c-5ce9114baeb3"), 300.00m, new Guid("08d3776c-eb98-434b-9d36-85fb057ca05b"), 3, 3 });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Address", "Description", "ImageUrl", "LandlordId", "PricePerWeek", "RenterId", "RoomCategoryId", "TownId" },
                values: new object[] { new Guid("717bb46a-06e2-4d4b-9b67-471424100ee1"), "53 Watson Lane", "Small cozy one-bed room with beautiful balcony and a live-in Landlord", "https://static.independent.co.uk/2021/07/27/08/20165319-4a072180-9f19-4240-8ff1-e94279ffcace.jpg?quality=75&width=982&height=726&auto=webp", new Guid("84354850-dc70-4ee8-826c-5ce9114baeb3"), 90.00m, null, 2, 2 });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Address", "Description", "ImageUrl", "LandlordId", "PricePerWeek", "RenterId", "RoomCategoryId", "TownId" },
                values: new object[] { new Guid("c3d04036-cba5-424b-8134-08e10fbd4fbc"), "1548 Colony Street", "Elegant room with garden view and small bathroom", "https://clavertonhotel.co.uk/wp-content/uploads/2015/10/King-Size-Four-Poster.jpg", new Guid("84354850-dc70-4ee8-826c-5ce9114baeb3"), 200.00m, null, 1, 1 });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "RatingDigit", "RoomId" },
                values: new object[,]
                {
                    { 1, 9, new Guid("c3d04036-cba5-424b-8134-08e10fbd4fbc") },
                    { 2, 7, new Guid("717bb46a-06e2-4d4b-9b67-471424100ee1") },
                    { 3, 10, new Guid("1dc7e29a-1d1a-4a4b-ae99-05c8f99922a5") }
                });

            migrationBuilder.InsertData(
                table: "Renters",
                columns: new[] { "Id", "Job", "PhoneNumber", "RoomId", "UserId" },
                values: new object[] { new Guid("08d3776c-eb98-434b-9d36-85fb057ca05b"), "Bartender", "085555555", new Guid("1dc7e29a-1d1a-4a4b-ae99-05c8f99922a5"), "3808cb45-d6fd-4604-9e32-f15574f56f8a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Renters",
                keyColumn: "Id",
                keyValue: new Guid("08d3776c-eb98-434b-9d36-85fb057ca05b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3808cb45-d6fd-4604-9e32-f15574f56f8a");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("1dc7e29a-1d1a-4a4b-ae99-05c8f99922a5"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("717bb46a-06e2-4d4b-9b67-471424100ee1"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("c3d04036-cba5-424b-8134-08e10fbd4fbc"));

            migrationBuilder.DeleteData(
                table: "Landlords",
                keyColumn: "Id",
                keyValue: new Guid("84354850-dc70-4ee8-826c-5ce9114baeb3"));

            migrationBuilder.DeleteData(
                table: "RoomCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Towns",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Towns",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Towns",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3ecf1600-5711-4b55-840a-9ba518a64005");
        }
    }
}
