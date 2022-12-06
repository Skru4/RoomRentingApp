using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomRentingApp.Infrastructure.Migrations
{
    public partial class RoomIsActiveAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "3808cb45-d6fd-4604-9e32-f15574f56f8a",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "42a51ddd-376a-44cb-9ee1-57964626886b", "AQAAAAEAACcQAAAAEOJXl/eUDbPtNldysCM683/iJ1JJKXTFqeCb3n7INFGQccTNbwGibL0VMSF3Za3W+Q==", "3397719b-4ee5-44c3-8602-a7a1ace56130" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "3ecf1600-5711-4b55-840a-9ba518a64005",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "8445d23b-835b-4ae4-973a-752325d8562d", "AQAAAAEAACcQAAAAEAv7P8ba0IWZf5Sc5XZkXW4xLs+EPVuuT2GN5WEOqVNfAbNU4mKvbsYzz4CCDfam7A==", "5d67d524-fa6c-44b4-994e-fa6c572336a6" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("1dc7e29a-1d1a-4a4b-ae99-05c8f99922a5"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("717bb46a-06e2-4d4b-9b67-471424100ee1"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("c3d04036-cba5-424b-8134-08e10fbd4fbc"),
                column: "IsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Rooms");

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
        }
    }
}
