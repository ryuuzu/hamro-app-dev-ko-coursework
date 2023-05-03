using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCRSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_request_model_and_update_user_and_role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestedBy",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "RequestedCar",
                table: "Requests",
                newName: "RequestedCarId");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "Requests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RequestedById",
                table: "Requests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ApprovedById",
                table: "Requests",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_BillingId",
                table: "Requests",
                column: "BillingId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestedById",
                table: "Requests",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestedCarId",
                table: "Requests",
                column: "RequestedCarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_ApprovedById",
                table: "Requests",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_RequestedById",
                table: "Requests",
                column: "RequestedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Billings_BillingId",
                table: "Requests",
                column: "BillingId",
                principalTable: "Billings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Cars_RequestedCarId",
                table: "Requests",
                column: "RequestedCarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_ApprovedById",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_RequestedById",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Billings_BillingId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Cars_RequestedCarId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ApprovedById",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_BillingId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequestedById",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequestedCarId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestedById",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "RequestedCarId",
                table: "Requests",
                newName: "RequestedCar");

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                table: "Requests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RequestedBy",
                table: "Requests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
