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
        protected readonly ICrudRepository<User> _repository;

        public SessionService(ICrudRepository<User> repository) => _repository = repository;

        public async Task<object> Create(string document, string password)
        {
            var user = (await _repository
                .FilterAsync(u => u.Document == document))
                .FirstOrDefault();

            if (user == null) throw new NotFoundException("Usuário não encontrado");
            if (!PasswordEncryptor.Compare(password, user.Password))
                throw new PasswordMismatchException("Usuário ou senha inválidos");

            var token = new TokenCreator().Create(user);

            return new { user = user, token = token };
        }
    }
}
