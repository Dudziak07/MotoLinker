using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoLinker.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInCategory_Context : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementCategories");

            migrationBuilder.AddColumn<int>(
                name: "AnnouncementId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AnnouncementId",
                table: "Categories",
                column: "AnnouncementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Announcements_AnnouncementId",
                table: "Categories",
                column: "AnnouncementId",
                principalTable: "Announcements",
                principalColumn: "AnnouncementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Announcements_AnnouncementId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AnnouncementId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "AnnouncementId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "AnnouncementCategories",
                columns: table => new
                {
                    AnnouncementsAnnouncementId = table.Column<int>(type: "int", nullable: false),
                    CategoriesCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementCategories", x => new { x.AnnouncementsAnnouncementId, x.CategoriesCategoryId });
                    table.ForeignKey(
                        name: "FK_AnnouncementCategories_Announcements_AnnouncementsAnnouncementId",
                        column: x => x.AnnouncementsAnnouncementId,
                        principalTable: "Announcements",
                        principalColumn: "AnnouncementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnouncementCategories_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementCategories_CategoriesCategoryId",
                table: "AnnouncementCategories",
                column: "CategoriesCategoryId");
        }
    }
}
