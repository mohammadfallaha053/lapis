using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapisApi.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogPostsProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SummaryAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SummaryEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OurSpecialistId = table.Column<int>(type: "int", nullable: false),
                    MetaTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaDescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaDescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPost_OurSpecialist_OurSpecialistId",
                        column: x => x.OurSpecialistId,
                        principalTable: "OurSpecialist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_OurSpecialistId",
                table: "BlogPost",
                column: "OurSpecialistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPost");
        }
    }
}
