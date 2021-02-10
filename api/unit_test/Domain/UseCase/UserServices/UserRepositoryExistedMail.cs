using api.Domain.UseCase.UserServices;
using api.Domain.ViewModel;
using api.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace unit_test.Domain.UseCase.UserServices
{
  public class UserRepositoryExistedMail : IUserRepository
  {
      public Task<ICollection<UserView>> All()
      {
        ICollection<UserView> list = new List<UserView>();
        return Task.FromResult<ICollection<UserView>>(list);
      }

      public Task<int> CountByEmail(string email)
      {
        return Task.FromResult<int>(1);
      }

      public Task<int> CountByIdAndEmail(int id, string email)
      {
        return Task.FromResult<int>(1);
      }

      public Task Delete(User user)
      {
          throw new NotImplementedException();
      }

    public Task<User> FindByEmailAndPassword(string email, string password)
    {
      return Task.FromResult<User>(new User());
    }

    public Task<User> FindById(int id)
    {
      return Task.FromResult<User>(new User());
    }

    public Task Save(User user)
    {
        throw new NotImplementedException();
    }

    public Task Update(User user)
    {
        throw new NotImplementedException();
    }
  }
}