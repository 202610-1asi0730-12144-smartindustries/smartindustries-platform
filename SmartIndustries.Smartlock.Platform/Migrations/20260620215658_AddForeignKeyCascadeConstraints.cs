using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartIndustries.Smartlock.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyCascadeConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "i_x_sites_organization_id",
                table: "sites",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "i_x_roles_organization_id",
                table: "roles",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "i_x_people_organization_id",
                table: "people",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "i_x_memberships_role_id",
                table: "memberships",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "i_x_access_groups_organization_id",
                table: "access_groups",
                column: "organization_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_access_groups__organization_organization_id",
                table: "access_groups",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_memberships__role_role_id",
                table: "memberships",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_people_organizations_organization_id",
                table: "people",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_roles__organization_organization_id",
                table: "roles",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_sites_organizations_organization_id",
                table: "sites",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_access_groups__organization_organization_id",
                table: "access_groups");

            migrationBuilder.DropForeignKey(
                name: "f_k_memberships__role_role_id",
                table: "memberships");

            migrationBuilder.DropForeignKey(
                name: "f_k_people_organizations_organization_id",
                table: "people");

            migrationBuilder.DropForeignKey(
                name: "f_k_roles__organization_organization_id",
                table: "roles");

            migrationBuilder.DropForeignKey(
                name: "f_k_sites_organizations_organization_id",
                table: "sites");

            migrationBuilder.DropIndex(
                name: "i_x_sites_organization_id",
                table: "sites");

            migrationBuilder.DropIndex(
                name: "i_x_roles_organization_id",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "i_x_people_organization_id",
                table: "people");

            migrationBuilder.DropIndex(
                name: "i_x_memberships_role_id",
                table: "memberships");

            migrationBuilder.DropIndex(
                name: "i_x_access_groups_organization_id",
                table: "access_groups");
        }
    }
}
