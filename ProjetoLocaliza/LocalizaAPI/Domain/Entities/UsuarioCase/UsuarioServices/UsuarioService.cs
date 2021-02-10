using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaAPI.Domain.Entities.UsuarioCase.UsuarioServices
{
    public class UsuarioService
    {

        private IUsuarioRepository repository;

        public async Task Save(Usuario usuario)
        {
            //if (user.Role == null) user.Role = UserRole.Editor;

            //if (user.Id > 0)
            //{
            //    var size = await repository.CountByIdAndEmail(user.Id, user.Email);
            //    if (size > 0) throw new UserUniqMail("Email já cadastrado");
            //    await repository.Update(user);
            //}
            //else
            //{
            //    var size = await repository.CountByEmail(user.Email);
            //    if (size > 0) throw new UserUniqMail("Email já cadastrado");
            //    await repository.Save(user);
            //}

            await repository.Save(usuario);
        }
    }
}
