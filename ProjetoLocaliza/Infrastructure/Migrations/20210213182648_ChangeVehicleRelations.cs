using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangeVehicleRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehiclesModels_VehiclesBrands_VehicleBrandId",
                table: "VehiclesModels");

            migrationBuilder.DropIndex(
                name: "IX_VehiclesModels_VehicleBrandId",
                table: "VehiclesModels");

            migrationBuilder.DropColumn(
                name: "VehicleBrandId",
                table: "VehiclesModels");

            migrationBuilder.AddColumn<int>(
                name: "VehicleBrandId",
                table: "Vehicles",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleBrandId",
                table: "Vehicles",
                column: "VehicleBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehiclesBrands_VehicleBrandId",
                table: "Vehicles",
                column: "VehicleBrandId",
                principalTable: "VehiclesBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehiclesBrands_VehicleBrandId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleBrandId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleBrandId",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "VehicleBrandId",
                table: "VehiclesModels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehiclesModels_VehicleBrandId",
                table: "VehiclesModels",
                column: "VehicleBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehiclesModels_VehiclesBrands_VehicleBrandId",
                table: "VehiclesModels",
                column: "VehicleBrandId",
                principalTable: "VehiclesBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
