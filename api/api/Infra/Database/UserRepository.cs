using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Domain.Entities;
using api.Domain.UseCase.UserServices;
using api.Domain.ViewModel;
using api.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace api.Domain.Infra.Database
{
    public class UserRepository : IUserRepository
    {
        private readonly EntityContext context;
        public UserRepository(EntityContext context)
        {
          this.context = context;
        }
        public async Task<int> CountByIdAndEmail(int id, string email)
        {
          return await context.Users.Where(u => u.Id != id && u.Email == email).CountAsync();
        }
        public async Task<int> CountByEmail(string email)
        {
          return await context.Users.Where(u => u.Email == email).CountAsync();
        }
        public async Task<User> FindById(int id)
        {
          return await context.Users.FindAsync(id);
        }
        public async Task Save(User user)
        {
          context.Users.Add(user);
          await context.SaveChangesAsync(); 
        }
        public async Task Update(User user)
        {
          context.Users.Update(user);
          await context.SaveChangesAsync(); 
        }
        public async Task<ICollection<UserView>> All()
        {
            return await context.Users.Select( u => new UserView {
                        Id = u.Id, 
                        Name = u.Name, 
                        Email = u.Email, 
                        Role = u.Role.ToString()
                    }).ToListAsync();
        }
        public async Task Delete(User user)
        {
          context.Users.Remove(user);
          await context.SaveChangesAsync(); 
        }

    public async Task<User> FindByEmailAndPassword(string email, string password)
    {
        return await context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
    }
  }
}
