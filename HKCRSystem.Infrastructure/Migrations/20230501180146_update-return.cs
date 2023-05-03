using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCRSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatereturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AcceptedBy",
                table: "Returns",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Returns_AcceptedBy",
                table: "Returns",
                column: "AcceptedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_RequestId",
                table: "Returns",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_AspNetUsers_AcceptedBy",
                table: "Returns",
                column: "AcceptedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_Requests_RequestId",
                table: "Returns",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_AspNetUsers_UserId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Returns_AspNetUsers_AcceptedBy",
                table: "Returns");

            migrationBuilder.DropForeignKey(
                name: "FK_Returns_Requests_RequestId",
                table: "Returns");

            migrationBuilder.DropIndex(
                name: "IX_Returns_AcceptedBy",
                table: "Returns");

            migrationBuilder.DropIndex(
                name: "IX_Returns_RequestId",
                table: "Returns");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_UserId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Attachments");

            migrationBuilder.AlterColumn<Guid>(
                name: "AcceptedBy",
                table: "Returns",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Offers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "UserAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAttachments", x => x.Id);
                });
        }
    }
}
