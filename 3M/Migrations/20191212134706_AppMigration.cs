using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _3M.Migrations
{
    public partial class AppMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    RecipientName = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    IsProcessed = table.Column<bool>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Count = table.Column<int>(nullable: false),
                    BillId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingItem_Bill_BillId",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageAddress = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Property_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_ProductId",
                table: "Property",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItem_BillId",
                table: "ShoppingItem",
                column: "BillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "ShoppingItem");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
