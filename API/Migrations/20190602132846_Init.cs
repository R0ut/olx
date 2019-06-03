using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    DateOfCreation = table.Column<DateTimeOffset>(nullable: false),
                    TableAdId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    AdId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Location = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    ProductionYear = table.Column<int>(nullable: false),
                    Fuel = table.Column<string>(nullable: true),
                    HorsePower = table.Column<int>(nullable: false),
                    Mileage = table.Column<int>(nullable: false),
                    IsDamaged = table.Column<bool>(nullable: false),
                    TableUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.AdId);
                    table.ForeignKey(
                        name: "FK_Ads_Users_TableUserId",
                        column: x => x.TableUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_TableUserId",
                table: "Ads",
                column: "TableUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TableAdId",
                table: "Users",
                column: "TableAdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Ads_TableAdId",
                table: "Users",
                column: "TableAdId",
                principalTable: "Ads",
                principalColumn: "AdId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Users_TableUserId",
                table: "Ads");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Ads");
        }
    }
}
