using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapisApi.Migrations
{
    /// <inheritdoc />
    public partial class Testimonial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCommissionAmount",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DebitCommissionAmount",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "PayPalCommissionAmount",
                table: "Settings");

            migrationBuilder.CreateTable(
                name: "Testimonial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StarsNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testimonial", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Testimonial");

            migrationBuilder.AddColumn<decimal>(
                name: "CreditCommissionAmount",
                table: "Settings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DebitCommissionAmount",
                table: "Settings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayPalCommissionAmount",
                table: "Settings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreditCommissionAmount", "DebitCommissionAmount", "PayPalCommissionAmount" },
                values: new object[] { 0m, 0m, 0m });
        }
    }
}
