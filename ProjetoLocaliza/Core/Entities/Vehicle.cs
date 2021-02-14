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

        public string Image { get; set; }

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

        public Guid VehicleModelId { get; set; }
        public virtual VehicleModel VehicleModel { get; set; }

        public Guid VehicleBrandId { get; set; }
        public virtual VehicleBrand VehicleBrand { get; set; }

        public Guid VehicleCategoryId { get; set; }
        public virtual VehicleCategory VehicleCategory { get; set; }

        public virtual ICollection<Quotation> Quotations { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
