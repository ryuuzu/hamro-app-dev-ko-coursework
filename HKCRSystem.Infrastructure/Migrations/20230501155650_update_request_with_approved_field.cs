using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCRSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_request_with_approved_field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Requests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Requests");
        }
    }
}
