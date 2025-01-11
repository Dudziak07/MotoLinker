using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoLinker.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementCategories_Category_CategoriesCategoryId",
                table: "AnnouncementCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Categories_CategoryId",
                table: "Announcements");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_CategoryId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Announcements");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementCategories_Categories_CategoriesCategoryId",
                table: "AnnouncementCategories",
                column: "CategoriesCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementCategories_Categories_CategoriesCategoryId",
                table: "AnnouncementCategories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Announcements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_CategoryId",
                table: "Announcements",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementCategories_Category_CategoriesCategoryId",
                table: "AnnouncementCategories",
                column: "CategoriesCategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Categories_CategoryId",
                table: "Announcements",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }
    }
}
