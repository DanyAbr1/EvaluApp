using ApiMysql.Models.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMysql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController: Controller
    {
        private readonly SistemacarrosContext _context;

        public UsuarioController(SistemacarrosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Usuario>> Get(string nombre, string pass)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Nombre1 == nombre && x.Contrasena == pass);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Usuario user)
        {
         
            if (user == null)
            {         
                return BadRequest("Debe enviar un usuario y contraseña.");
            }

            if (string.IsNullOrWhiteSpace(user.Nombre1) || string.IsNullOrWhiteSpace(user.Contrasena))
            {                
                return BadRequest("Usuario o contraseña incorrecto");
            }

            var usuario = Authenticate(user.Nombre1, user.Contrasena);
            if (usuario == null)
            {                
                return BadRequest(new { message = "El usuario y/o password son incorrectos" });
            }
           
            return Ok(usuario);
        }


        private Usuario Authenticate(string login, string password)
        {

            Usuario usuario = _context.Usuario.FirstOrDefault(o => o.Nombre1 == login && o.Contrasena == password );
            if (usuario == null)
            {
                return null;
            }


            // remove password before returning
            usuario.Contrasena = null;

            return usuario;
        }

    }
}
