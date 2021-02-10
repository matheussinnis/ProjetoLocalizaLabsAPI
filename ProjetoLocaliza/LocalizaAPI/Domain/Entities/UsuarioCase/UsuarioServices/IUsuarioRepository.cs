using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaAPI.Domain.Entities.UsuarioCase.UsuarioServices
{
    public interface IUsuarioRepository
    {
        Task Save(Usuario usuario);
    }
}
