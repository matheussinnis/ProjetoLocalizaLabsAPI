using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Domain.Interfaces
{
    public interface ISessionService
    {
        Task<object> Create(string document, string password);
    }
}
