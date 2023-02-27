using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGroups_ProductGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ProductGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceWithVat = table.Column<decimal>(type: "TEXT", nullable: false),
                    VatRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreProducts",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false),
                    StoresId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProducts", x => new { x.ProductsId, x.StoresId });
                    table.ForeignKey(
                        name: "FK_StoreProducts_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreProducts_Stores_StoresId",
                        column: x => x.StoresId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductGroups",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, "Group 1", null },
                    { 2, "Group 2", null }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Store 1" },
                    { 2, "Store 2" },
                    { 3, "Store 3" }
                });

            migrationBuilder.InsertData(
                table: "ProductGroups",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[,]
                {
                    { 3, "Group 1-3", 1 },
                    { 4, "Group 1-4", 1 },
                    { 5, "Group 2-5", 2 },
                    { 6, "Group 2-6", 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Name", "Price", "PriceWithVat", "ProductGroupId", "VatRate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529), "Product 1", 10.00m, 12.00m, 1, 0.2m },
                    { 2, new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529), "Product 2", 5.00m, 6.00m, 1, 0.2m },
                    { 3, new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529), "Product 3", 7.35m, 8.82m, 2, 0.2m }
                });

            migrationBuilder.InsertData(
                table: "ProductGroups",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[] { 7, "Group 1-3-7", 3 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Name", "Price", "PriceWithVat", "ProductGroupId", "VatRate" },
                values: new object[,]
                {
                    { 4, new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529), "Product 4", 102.50m, 111.73m, 3, 0.09m },
                    { 5, new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529), "Product 5", 24.95m, 29.94m, 4, 0.2m },
                    { 6, new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529), "Product 6", 2.10m, 2.52m, 5, 0.2m },
                    { 7, new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529), "Product 7", 15.20m, 16.57m, 6, 0.09m }
                });

            migrationBuilder.InsertData(
                table: "StoreProducts",
                columns: new[] { "ProductsId", "StoresId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Name", "Price", "PriceWithVat", "ProductGroupId", "VatRate" },
                values: new object[,]
                {
                    { 8, new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529), "Product 8", 50.00m, 60.00m, 7, 0.2m },
                    { 9, new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529), "Product 9", 35.40m, 38.59m, 7, 0.09m }
                });

            migrationBuilder.InsertData(
                table: "StoreProducts",
                columns: new[] { "ProductsId", "StoresId" },
                values: new object[,]
                {
                    { 4, 2 },
                    { 5, 3 },
                    { 6, 3 },
                    { 7, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_ParentId",
                table: "ProductGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductGroupId",
                table: "Products",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_StoresId",
                table: "StoreProducts",
                column: "StoresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreProducts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "ProductGroups");
        }
    }
}
