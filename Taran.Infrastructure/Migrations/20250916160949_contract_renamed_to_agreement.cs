using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taran.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class contract_renamed_to_agreement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract",
                schema: "contract");

            migrationBuilder.CreateTable(
                name: "Agreement",
                schema: "contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    AgreementDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DurationInMonths = table.Column<int>(type: "int", nullable: false),
                    ExtraUsersCount = table.Column<int>(type: "int", nullable: false),
                    CreatorUserId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EditorUserId = table.Column<int>(type: "int", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agreement_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "contract",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agreement_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "contract",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_CustomerId",
                schema: "contract",
                table: "Agreement",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_ProductId",
                schema: "contract",
                table: "Agreement",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agreement",
                schema: "contract");

            migrationBuilder.CreateTable(
                name: "Contract",
                schema: "contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AgreementDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatorUserId = table.Column<int>(type: "int", nullable: false),
                    DurationInMonths = table.Column<int>(type: "int", nullable: false),
                    EditorUserId = table.Column<int>(type: "int", nullable: true),
                    ExtraUsersCount = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "contract",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "contract",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CustomerId",
                schema: "contract",
                table: "Contract",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ProductId",
                schema: "contract",
                table: "Contract",
                column: "ProductId");
        }
    }
}
