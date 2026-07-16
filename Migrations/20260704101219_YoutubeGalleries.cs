using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapisApi.Migrations
{
    /// <inheritdoc />
    public partial class YoutubeGalleries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FAQsTabToggle",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GalleriesTabToggle",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OurSpecialistsTabToggle",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ServicesTabToggle",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TestimonialsTabToggle",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "YouTubeGalleriesTabToggle",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "YouTubeGallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YouTubeGallery", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FAQsTabToggle", "GalleriesTabToggle", "OurSpecialistsTabToggle", "ServicesTabToggle", "TestimonialsTabToggle", "YouTubeGalleriesTabToggle" },
                values: new object[] { true, true, false, true, true, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YouTubeGallery");

            migrationBuilder.DropColumn(
                name: "FAQsTabToggle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "GalleriesTabToggle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "OurSpecialistsTabToggle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "ServicesTabToggle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TestimonialsTabToggle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "YouTubeGalleriesTabToggle",
                table: "Settings");
        }
    }
}
