using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Database.Dtos;

namespace Domain.Interfaces
{
    public interface IAgencyService : IBaseService<Agency>
    {
        public Task<IEnumerable<AvailableVehicleDto>> GetAvailableVehicles(
            string agencyId, bool available, DateTime withdrawalDate, int page, int perPage
        );

        public Task<IEnumerable<dynamic>> GetNearestAgencies(
            float latitude, float longitude
        );
    }
}
