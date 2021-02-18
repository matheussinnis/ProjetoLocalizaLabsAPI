using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Interfaces;
using Infrastructure.Database.Interfaces;

namespace Domain.Services
{
    public class VehicleService : BaseService<Vehicle>, IVehicleService
    {
        public new IVehicleRepository _repository { get; set; }

        public VehicleService(IVehicleRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Task<List<Vehicle>> GetAvailableVehicles(string agencyId, DateTime withdrawalDate)
        {
            return _repository.GetAvailableVehicles(new Guid(agencyId), withdrawalDate);
        }
    }
}
