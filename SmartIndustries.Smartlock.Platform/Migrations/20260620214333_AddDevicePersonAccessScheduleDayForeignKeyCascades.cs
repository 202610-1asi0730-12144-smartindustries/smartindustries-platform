using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartIndustries.Smartlock.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddDevicePersonAccessScheduleDayForeignKeyCascades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "i_x_schedule_days_person_id",
                table: "schedule_days",
                column: "person_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_person_accesses__person_person_id",
                table: "person_accesses",
                column: "person_id",
                principalTable: "people",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_schedule_days__person_person_id",
                table: "schedule_days",
                column: "person_id",
                principalTable: "people",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_person_accesses__person_person_id",
                table: "person_accesses");

            migrationBuilder.DropForeignKey(
                name: "f_k_schedule_days__person_person_id",
                table: "schedule_days");

            migrationBuilder.DropIndex(
                name: "i_x_schedule_days_person_id",
                table: "schedule_days");
        }
    }
}
