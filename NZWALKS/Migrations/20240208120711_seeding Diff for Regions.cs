using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWALKS.Migrations
{
    /// <inheritdoc />
    public partial class seedingDiffforRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a819730b-fe96-49f4-bf91-9d73e560f96d"), "Easy" },
                    { new Guid("ad5f514d-15a9-42b6-b032-6e000b812ab2"), "Easy" },
                    { new Guid("d572fe86-b1b5-42c2-8f27-8f650b44bd4a"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageURI" },
                values: new object[,]
                {
                    { new Guid("57b59cc8-2fe0-4308-8249-c4b18027a080"), "NL", "Northland", "https://www.doc.govt.nz/globalassets/images/places/northland/northland.jpg" },
                    { new Guid("94c2967b-8dad-46c7-847b-2b233c8d8800"), "AK", "Auckland", "https://www.doc.govt.nz/globalassets/images/places/auckland/auckland.jpg" },
                    { new Guid("f9331faf-6ac3-4bf8-a0bc-9132bd9f4766"), "WK", "Waikato", "https://www.doc.govt.nz/globalassets/images/places/waikato/waikato.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a819730b-fe96-49f4-bf91-9d73e560f96d"));

            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "Id",
                keyValue: new Guid("ad5f514d-15a9-42b6-b032-6e000b812ab2"));

            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d572fe86-b1b5-42c2-8f27-8f650b44bd4a"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: new Guid("57b59cc8-2fe0-4308-8249-c4b18027a080"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: new Guid("94c2967b-8dad-46c7-847b-2b233c8d8800"));

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: new Guid("f9331faf-6ac3-4bf8-a0bc-9132bd9f4766"));
        }
    }
}
