using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Domain.Interfaces
{
    public interface IScheduleService : IBaseService<Schedule>
    {
        public Task<IEnumerable<Schedule>> GetUserSchedulesAsync(
            string userId, string currentUserDocument
        );
    }
}
