using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Domain.Interfaces
{
    public interface IVehicleService : IBaseService<Vehicle>
    {
        public Task<List<Vehicle>> GetAvailableVehicles();
    }
}
