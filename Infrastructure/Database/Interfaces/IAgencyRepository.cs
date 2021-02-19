using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Database.Dtos;

namespace Infrastructure.Database.Interfaces
{
    public interface IAgencyRepository : ICrudRepository<Agency>
    {
        Task<List<AvailableVehicleDto>> GetAvailableVehicles(
            Guid agencyId, DateTime withdrawalDate, int page, int perPage
        );
    }
}
