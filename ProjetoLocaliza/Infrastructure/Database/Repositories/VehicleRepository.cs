using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(DataContext context) : base(context) {}

        public Task<List<Vehicle>> GetAvailableVehicles()
        {
            var query = from vehicle in _context.Vehicles
                join schedule in _context.Schedules on vehicle.Id equals schedule.VehicleId
                into joinTable
                from subschedule in joinTable.DefaultIfEmpty()
                where subschedule.RealReturnDate != null || subschedule.Id == null
                select vehicle;

            return query.ToListAsync();
        }
    }
}
