using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class FixVehicleAgencyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AgencyId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Agency",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agency", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_AgencyId",
                table: "Vehicles",
                column: "AgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Agency_AgencyId",
                table: "Vehicles",
                column: "AgencyId",
                principalTable: "Agency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Agency_AgencyId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Agency");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_AgencyId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "AgencyId",
                table: "Vehicles");
        }
    }
}
