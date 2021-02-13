using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Vehicle : BaseEntity
    {
        [Required]
        public string PlateLicense { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public FuelType FuelType { get; set; }

        [Required]
        public decimal HourlyPrice { get; set; }

        [Required]
        public int TrunkCapacity { get; set; }

        [Required]
        public int TankCapacity { get; set; }

        [Required]
        public VehicleModel VehicleModel { get; set; }

        [Required]
        public VehicleBrand VehicleBrand { get; set; }

        [Required]
        public VehicleCategory VehicleCategory { get; set; }
    }
}
