using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        private Expression<Func<Schedule, bool>> MountGetUserSchedulesExpression(
            Guid userId, string startDate, string endDate
        )
        {
            if (startDate != null && endDate != null)
                return schedule => schedule.UserId == userId
                    && schedule.CreatedAt >= DateTime.Parse(startDate)
                    && schedule.CreatedAt <= DateTime.Parse(endDate);

            if (startDate != null)
                return schedule => schedule.UserId == userId
                    && schedule.CreatedAt >= DateTime.Parse(startDate);

            if (endDate != null)
                return schedule => schedule.UserId == userId
                    && schedule.CreatedAt <= DateTime.Parse(endDate);

            return schedule => schedule.UserId == userId;
        }

        public async Task<IEnumerable<Schedule>> GetUserSchedulesAsync(
            string userId, string currentUserId, string startDate, string endDate
        )
        {
            var userIdGuid = new Guid(userId);
            var currentUser = await _userRepository.FindAsync(currentUserId);

            if (currentUser.Type == UserType.Customer && userIdGuid != currentUser.Id)
                throw new UnauthorizedException("Não é possível listar agendamentos de outros usuários");

            return await _repository.FilterAsync(MountGetUserSchedulesExpression(
                userIdGuid, startDate, endDate
            ));
        }
    }
}
