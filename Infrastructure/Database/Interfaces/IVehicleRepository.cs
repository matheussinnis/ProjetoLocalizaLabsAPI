using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Infrastructure.Database.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Task<List<Vehicle>> GetAvailableVehicles(Guid agencyId);
    }
}
