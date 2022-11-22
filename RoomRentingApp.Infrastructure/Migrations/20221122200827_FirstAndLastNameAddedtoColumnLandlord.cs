using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomRentingApp.Infrastructure.Migrations
{
    public partial class FirstAndLastNameAddedtoColumnLandlord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Landlords",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Landlords",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3808cb45-d6fd-4604-9e32-f15574f56f8a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ae8d313-c0b8-4eeb-898d-ed228762da4b", "AQAAAAEAACcQAAAAEMG8luSX/xK0cDOKnKCFb7bMg9zeJmPWLp9jb4MKVHbLTG2bY8SMFWklg3k2UYiihw==", "477591ae-a716-4b8c-bb2e-e205589db691" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3ecf1600-5711-4b55-840a-9ba518a64005",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1b8f3fc-7aef-46d0-8158-f2253cd8dd32", "AQAAAAEAACcQAAAAEDU8b6OTauMsL+0zYnk6TI685rOYEjNQDCCZxMrcwghOApSWTP/vL5n2PlZBOA3AlA==", "8ce1058c-cd57-4330-ba56-a2bb5d2b4907" });

            migrationBuilder.UpdateData(
                table: "Landlords",
                keyColumn: "Id",
                keyValue: new Guid("84354850-dc70-4ee8-826c-5ce9114baeb3"),
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Ivan", "Ivanov" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Landlords");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Landlords");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3808cb45-d6fd-4604-9e32-f15574f56f8a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3077d59-f4a4-4d37-960a-be1c1d7586a2", "AQAAAAEAACcQAAAAEN0nMFDa8wQBYMVeKf89H2UXlvrDJX1NV5GaNpkXUcosp6UoE+X4besmPbagDCIqzQ==", "5a0f970b-2a2d-442f-b34e-7f5ae0060bf2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3ecf1600-5711-4b55-840a-9ba518a64005",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0869f43-c3da-4f00-a282-8035199857b3", "AQAAAAEAACcQAAAAEFHBinYZhG9nZm4by42aeGs4JY4eq6LNexAfn1+D5FZTZI96ijJzA/badE0Xe85TwA==", "40a0103b-6cc1-4138-befc-d68fb4da4168" });
        }
    }
}
