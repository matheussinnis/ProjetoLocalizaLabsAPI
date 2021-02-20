using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Interfaces;
using Infrastructure.Database.Dtos;
using Infrastructure.Database.Interfaces;

namespace Domain.Services
{
    public class AgencyService : BaseService<Agency>, IAgencyService
    {
        private new IAgencyRepository _repository { get; set; }
        private ICrudRepository<Vehicle> _vehicleRepository { get; set; }

        public AgencyService(
            IAgencyRepository repository,
            ICrudRepository<Vehicle> vehicleRepository
        ) : base(repository)
        {
            _repository = repository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<AvailableVehicleDto>> GetVehicles(
            string agencyId, bool available, DateTime withdrawalDate,
            int page = 1, int perPage = 10
        )
        {
            var agencyGuid = new Guid(agencyId);
            if (!available)
                return (
                    await _vehicleRepository
                    .Limit(perPage)
                    .Skip((page - 1) * perPage)
                    .FilterAsync(
                        v => v.AgencyId == agencyGuid,
                        include => include.VehicleBrand,
                        include => include.VehicleCategory,
                        include => include.VehicleModel
                    )
                ).Select(v => new AvailableVehicleDto()
                    {
                        Id = v.Id,
                        FuelTypeName = v.FuelType.ToString(),
                        HourlyPrice = v.HourlyPrice,
                        Image = v.Image,
                        PlateLicense = v.PlateLicense,
                        TankCapacity = v.TankCapacity,
                        TrunkCapacity = v.TrunkCapacity,
                        VehicleBrandName = v.VehicleBrand.Name,
                        VehicleCategoryName = v.VehicleCategory.Name,
                        VehicleModelName = v.VehicleModel.Name,
                        Year = v.Year,
                    });

            return (await _repository.GetAvailableVehicles(
                agencyGuid, withdrawalDate, page, perPage
            )).AsEnumerable();
        }

        public Task<IEnumerable<dynamic>> GetNearestAgencies(
            float latitude, float longitude
        )
        {
            return _repository.CollectionFromSql(@$"
with cteAgency as
(
    SELECT Agency.*, (
        6371
        * acos(
            cos(radians({latitude}))
            * cos(radians(Latitude))
            * cos(radians(Longitude) - radians({longitude}))
            + sin(radians({latitude})) * sin(radians(Latitude))
        )
    ) AS Distance
    FROM Agency
)

SELECT TOP 10 * FROM cteAgency
WHERE Distance < 20
ORDER BY Distance
");
        }
    }
}
