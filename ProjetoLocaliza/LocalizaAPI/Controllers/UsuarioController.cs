using LocalizaAPI.Domain.Entities;
using LocalizaAPI.Domain.Entities.UsuarioCase.UsuarioServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaAPI.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly UsuarioService _userioService;

        [HttpPost]
        [Route("/usuario/cadastro")]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastro([FromBody] Usuario usuario)
        {
            try
            {
                await _userioService.Save(usuario);
                return StatusCode(201);
            }
            catch (Exception exception)
            {
                return StatusCode(401, new{Message = exception.Message});
            }
        }
    }
}
