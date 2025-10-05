using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taran.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Notification_Entity_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                schema: "contract",
                table: "MessageTemplate",
                newName: "Template");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ExpireDate",
                schema: "contract",
                table: "Agreement",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "contract",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgreementId = table.Column<int>(type: "int", nullable: false),
                    MessageTemplateId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NotificationStatus = table.Column<int>(type: "int", nullable: false),
                    LastTryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TryCount = table.Column<int>(type: "int", nullable: false),
                    ErrorDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatorUserId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditorUserId = table.Column<int>(type: "int", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Agreement_AgreementId",
                        column: x => x.AgreementId,
                        principalSchema: "contract",
                        principalTable: "Agreement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_MessageTemplate_MessageTemplateId",
                        column: x => x.MessageTemplateId,
                        principalSchema: "contract",
                        principalTable: "MessageTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name",
                schema: "contract",
                table: "Product",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageTemplate_DaysUntilAgreementExpire",
                schema: "contract",
                table: "MessageTemplate",
                column: "DaysUntilAgreementExpire",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_AgreementId",
                schema: "contract",
                table: "Notification",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_MessageTemplateId",
                schema: "contract",
                table: "Notification",
                column: "MessageTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification",
                schema: "contract");

            migrationBuilder.DropIndex(
                name: "IX_Product_Name",
                schema: "contract",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_MessageTemplate_DaysUntilAgreementExpire",
                schema: "contract",
                table: "MessageTemplate");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                schema: "contract",
                table: "Agreement");

            migrationBuilder.RenameColumn(
                name: "Template",
                schema: "contract",
                table: "MessageTemplate",
                newName: "Message");
        }
    }
}
