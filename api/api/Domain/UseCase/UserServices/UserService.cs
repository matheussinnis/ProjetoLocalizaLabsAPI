using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Domain.Authentication;
using api.Domain.Entities;
using api.Domain.ViewModel;

namespace api.Domain.UseCase.UserServices
{
    public class UserService
    {
        public UserService(IUserRepository repository)
        {
          this.repository = repository;
        }
        private IUserRepository repository;
        public async Task Save(User user)
        {
          if(user.Role == null) user.Role = UserRole.Editor;
          
          if(user.Id > 0)
          {
            var size = await repository.CountByIdAndEmail(user.Id, user.Email);
            if(size > 0) throw new UserUniqMail("Email já cadastrado");
            await repository.Update(user);
          }
          else
          {
            var size = await repository.CountByEmail(user.Email);
            if(size > 0) throw new UserUniqMail("Email já cadastrado");
            await repository.Save(user);
          }
        }
        public async Task Delete(int id)
        {
          if(id == 0) throw new UserEmptyId("Id não pode ser vazio");
          var user = await repository.FindById(id);
          if(user == null) throw new UserNotFound("Usuário não encontrado");
          await repository.Delete(user);
        }
        public async Task<UserJwt> Login(User user, IToken token)
        {
           var loggedUser = await repository.FindByEmailAndPassword(user.Email, user.Password);
           if(loggedUser == null) throw new UserNotFound("Usuário e senha inválidos");
           return new UserJwt(){
             Id = loggedUser.Id,
             Name = loggedUser.Name,
             Email = loggedUser.Email,
             Role = loggedUser.Role.ToString(),
             Token = token.GerarToken(loggedUser)
           };
        }

        public Task<ICollection<UserView>> All()
        {
           return repository.All();
        }
    }
}
