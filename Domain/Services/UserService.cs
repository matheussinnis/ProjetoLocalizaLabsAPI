using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Auth;
using Infrastructure.Database.Interfaces;

namespace Domain.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IBaseRepository<User> repository) : base(repository) {}

        public new async Task<User> AddAsync(User quotation)
        {
            quotation.Password = PasswordEncryptor.Encrypt(quotation.Password);
            await _repository.AddAsync(quotation);
            await _repository.SaveChangesAsync();
            return quotation;
        }

        public override async Task<User> FindAsync(
            string id,
            params Expression<Func<User, object>>[] includes
        )
        {
            var entity = await _repository.FindAsync(id, includes);
            if (entity == null)
                throw new NotFoundException($"{typeof(User).Name} não encontrado(a)");

            return entity;
        }
    }
}
