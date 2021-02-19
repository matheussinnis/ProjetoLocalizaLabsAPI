using System;

namespace Infrastructure.Database.Dtos
{
    public class AvailableVehicleDto
    {
        public Guid Id { get; set; }
        public string PlateLicense { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
        public string FuelTypeName { get; set; }
        public decimal HourlyPrice { get; set; }
        public int TrunkCapacity { get; set; }
        public int TankCapacity { get; set; }
        public string VehicleModelName { get; set; }
        public string VehicleBrandName { get; set; }
        public string VehicleCategoryName { get; set; }
    }
}
