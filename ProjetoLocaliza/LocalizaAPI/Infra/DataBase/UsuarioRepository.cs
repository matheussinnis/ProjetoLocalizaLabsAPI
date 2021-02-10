using LocalizaAPI.Domain.Entities;
using LocalizaAPI.Domain.Entities.UsuarioCase.UsuarioServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LocalizaAPI.Infra.DataBase
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly EntityContext contexto;

        public UsuarioRepository(EntityContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task Save(Usuario usuario)
        {
            contexto.Usuario.Add(usuario);
            await contexto.SaveChangesAsync();
        }



    }
}
