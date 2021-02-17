using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Database.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(DataContext context) : base(context) { }

        public Task<List<Vehicle>> GetAvailableVehicles(Guid agencyId)
        {
            var query = (from vehicle in _context.Vehicles
                        join schedule in _context.Schedules on vehicle.Id equals schedule.VehicleId
                        into joinTable
                        from subschedule in joinTable.DefaultIfEmpty()
                        where vehicle.AgencyId == agencyId
                        && (subschedule.RealReturnDate != null || subschedule.Id == null)
                        select vehicle).Distinct();

            return query.ToListAsync();
        }
    }
}
