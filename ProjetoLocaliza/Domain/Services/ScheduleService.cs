using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Database.Interfaces;

namespace Domain.Services
{
    public class ScheduleService : BaseService<Schedule>, IScheduleService
    {
        public IBaseRepository<User> _userRepository { get; set; }
        public IBaseRepository<Vehicle> _vehicleRepository { get; set; }
        public IBaseRepository<Quotation> _quotationRepository { get; set; }

        public ScheduleService(
            IBaseRepository<Schedule> repository,
            IBaseRepository<User> userRepository,
            IBaseRepository<Vehicle> vehicleRepository,
            IBaseRepository<Quotation> quotationRepository
        ) : base(repository)
        {
            _userRepository = userRepository;
            _vehicleRepository = vehicleRepository;
            _quotationRepository = quotationRepository;
        }

        private async Task SetScheduleHourlyPrice(Schedule schedule)
        {
            if (schedule.QuotationId != null)
            {
                var quotation = await _quotationRepository.FindAsync(schedule.QuotationId.ToString());
                schedule.HourlyPrice = quotation.HourlyPrice;
            }
            else
            {
                var vehicle = await _vehicleRepository.FindAsync(schedule.VehicleId.ToString());
                schedule.HourlyPrice = vehicle.HourlyPrice;
            }
        }

        public new async Task<Schedule> AddAsync(Schedule schedule)
        {
            await SetScheduleHourlyPrice(schedule);
            schedule.Checklist = new Checklist();
            await _repository.AddAsync(schedule);
            await _repository.SaveChangesAsync();
            return schedule;
        }

        public new async Task<Schedule> UpdateAsync(Schedule schedule)
        {
            await SetScheduleHourlyPrice(schedule);
            await _repository.AddAsync(schedule);
            await _repository.SaveChangesAsync();
            return schedule;
        }

        public async Task<IEnumerable<Schedule>> GetUserSchedulesAsync(
            string userId, string currentUserDocument
        )
        {
            var currentUser = (
                await _userRepository.FilterAsync(user => user.Document == currentUserDocument)
            ).First();

            if (currentUser.Type == UserType.Customer && userId != currentUser.Id.ToString())
                throw new UnauthorizedException("Não é possível listar agendamentos de outros usuários");

            var guid = new Guid(userId);
            return await _repository.FilterAsync(schedule => schedule.UserId == guid);
        }
    }
}
