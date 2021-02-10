using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain.Entities;
using api.Domain.ViewModel;

namespace api.Domain.UseCase.UserServices
{
    public interface IUserRepository
    {
        Task<int> CountByIdAndEmail(int id, string email);
        Task<int> CountByEmail(string email);
        Task<User> FindById(int id);
        Task Update(User user);
        Task Save(User user);
        Task Delete(User user);
        Task<ICollection<UserView>> All();
        Task<User> FindByEmailAndPassword(string email, string password);
    }
}
