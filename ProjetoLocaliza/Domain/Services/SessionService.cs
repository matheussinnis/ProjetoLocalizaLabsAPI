using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Auth;
using Infrastructure.Database.Interfaces;

namespace Domain.Services
{
    public class SessionService : ISessionService
    {
        protected readonly IBaseRepository<User> _repository;

        public SessionService(IBaseRepository<User> repository) => _repository = repository;

        public async Task<object> Create(string document, string password)
        {
            var user = (await _repository.FilterAsync(
                user => user.Document == document && user.Password == password)
            ).FirstOrDefault();

            if (user == null) throw new NotFoundException("Usuário ou senha inválidos");

            var token = new TokenCreator().Create(user);

            return new { user = user, token = token };
        }
    }
}
