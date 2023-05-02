using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCRSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedamage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Damages_BillingId",
                table: "Damages",
                column: "BillingId");

            migrationBuilder.CreateIndex(
                name: "IX_Damages_RequestId",
                table: "Damages",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Damages_Billings_BillingId",
                table: "Damages",
                column: "BillingId",
                principalTable: "Billings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Damages_Requests_RequestId",
                table: "Damages",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Damages_Billings_BillingId",
                table: "Damages");

            migrationBuilder.DropForeignKey(
                name: "FK_Damages_Requests_RequestId",
                table: "Damages");

            migrationBuilder.DropIndex(
                name: "IX_Damages_BillingId",
                table: "Damages");

            migrationBuilder.DropIndex(
                name: "IX_Damages_RequestId",
                table: "Damages");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Requests");
        }
    }
}
