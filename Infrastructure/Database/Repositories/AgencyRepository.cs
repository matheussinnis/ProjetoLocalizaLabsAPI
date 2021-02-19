using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using Infrastructure.Database.Dtos;

namespace Infrastructure.Database.Repositories
{
    public class AgencyRepository : CrudRepository<Agency>, IAgencyRepository
    {
        public AgencyRepository(DataContext context) : base(context) { }

        public Task<List<AvailableVehicleDto>> GetAvailableVehicles(
            Guid agencyId, DateTime withdrawalDate, int page, int perPage
        )
        {
            var query = (
                from vehicle in _context.Vehicles
                join vehicleModel in _context.VehiclesModels on vehicle.VehicleModelId equals vehicleModel.Id
                join vehicleCategory in _context.VehiclesCategorys on vehicle.VehicleCategoryId equals vehicleCategory.Id
                join vehicleBrand in _context.VehiclesBrands on vehicle.VehicleBrandId equals vehicleBrand.Id
                join schedule in _context.Schedules on vehicle.Id equals schedule.VehicleId
                into joinTable
                from subschedule in joinTable.DefaultIfEmpty()
                where vehicle.AgencyId == agencyId && (
                    subschedule.ExpectedReturnDate.AddHours(2) <= withdrawalDate
                    || subschedule.Id == null
                )
                select new AvailableVehicleDto()
                {
                    Id = vehicle.Id,
                    FuelTypeName = vehicle.FuelType.ToString(),
                    HourlyPrice = vehicle.HourlyPrice,
                    Image = vehicle.Image,
                    PlateLicense = vehicle.PlateLicense,
                    TankCapacity = vehicle.TankCapacity,
                    TrunkCapacity = vehicle.TrunkCapacity,
                    VehicleBrandName = vehicle.VehicleBrand.Name,
                    VehicleCategoryName = vehicle.VehicleCategory.Name,
                    VehicleModelName = vehicle.VehicleModel.Name,
                    Year = vehicle.Year,
                }
            ).Distinct().Skip((page - 1) * perPage).Take(perPage);

            return query.ToListAsync();
        }
    }
}
