using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taran.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MessageTemplate_Entities_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Customer_CustomerId",
                schema: "contract",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Product_ProductId",
                schema: "contract",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleAccess_Role_RoleId",
                schema: "identity",
                table: "RoleAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "identity",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                schema: "identity",
                table: "UserRole");

            migrationBuilder.CreateTable(
                name: "CommunicationConfig",
                schema: "contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    CreatorUserId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EditorUserId = table.Column<int>(type: "int", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageTemplate",
                schema: "contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar", nullable: false),
                    Message = table.Column<string>(type: "varchar", nullable: false),
                    DaysUntilAgreementExpire = table.Column<int>(type: "int", nullable: false),
                    CreatorUserId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EditorUserId = table.Column<int>(type: "int", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceHolder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar", nullable: false),
                    Path = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceHolder", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Customer_CustomerId",
                schema: "contract",
                table: "Agreement",
                column: "CustomerId",
                principalSchema: "contract",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Product_ProductId",
                schema: "contract",
                table: "Agreement",
                column: "ProductId",
                principalSchema: "contract",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAccess_Role_RoleId",
                schema: "identity",
                table: "RoleAccess",
                column: "RoleId",
                principalSchema: "identity",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "identity",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "identity",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                schema: "identity",
                table: "UserRole",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Customer_CustomerId",
                schema: "contract",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Product_ProductId",
                schema: "contract",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleAccess_Role_RoleId",
                schema: "identity",
                table: "RoleAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "identity",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                schema: "identity",
                table: "UserRole");

            migrationBuilder.DropTable(
                name: "CommunicationConfig",
                schema: "contract");

            migrationBuilder.DropTable(
                name: "MessageTemplate",
                schema: "contract");

            migrationBuilder.DropTable(
                name: "PlaceHolder");

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Customer_CustomerId",
                schema: "contract",
                table: "Agreement",
                column: "CustomerId",
                principalSchema: "contract",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Product_ProductId",
                schema: "contract",
                table: "Agreement",
                column: "ProductId",
                principalSchema: "contract",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAccess_Role_RoleId",
                schema: "identity",
                table: "RoleAccess",
                column: "RoleId",
                principalSchema: "identity",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "identity",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "identity",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                schema: "identity",
                table: "UserRole",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
