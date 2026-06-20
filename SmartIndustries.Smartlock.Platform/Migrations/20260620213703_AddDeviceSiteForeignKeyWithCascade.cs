using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartIndustries.Smartlock.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceSiteForeignKeyWithCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "i_x_devices_site_id",
                table: "devices",
                column: "site_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_devices__site_site_id",
                table: "devices",
                column: "site_id",
                principalTable: "sites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_devices__site_site_id",
                table: "devices");

            migrationBuilder.DropIndex(
                name: "i_x_devices_site_id",
                table: "devices");
        }
    }
}
