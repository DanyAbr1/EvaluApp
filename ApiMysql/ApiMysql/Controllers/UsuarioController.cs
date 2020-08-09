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
        private string _resultado;

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



        
        private  Usuario Authenticate(string login, string password)
        {

            Usuario usuario =  _context.Usuario.FirstOrDefault(o => o.Nombre1 == login && o.Contrasena == password);
            if (usuario == null)
            {
                return null;
            }


            // remove password before returning
            usuario.Contrasena = null;

            return usuario;
        }

        [HttpPost("AddUser")]
        public IActionResult Post([FromBody] Usuario vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = ValidarModelo(vm);
                if (!resultado)
                {
                    return BadRequest(_resultado);
                }

                

                    _context.Usuario.Update(vm);
                    _context.SaveChanges();
                    return NoContent();
                

            }
            catch (Exception e)
            {                
                return StatusCode(500, "No se pudo completar la operación.");
            }
        }

        private bool ValidarModelo(Usuario vm)
        {
            var valid = true;

            if (string.IsNullOrWhiteSpace(vm.Nombre2))
            {
                _resultado = "Debe especificar el nombre.";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(vm.Apellido1))
            {
                _resultado = "Debe especificar el apellido.";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(vm.Nombre1))
            {
                _resultado = "Debe especificar el Usuario.";
                valid = false;
            }

            //var obj1 = _context.SolicitudUsuario.FirstOrDefault(o => o.SolicitudUsuarioId != vm.SolicitudUsuarioId &&
            //o.NombreEstablecimiento == vm.NombreEstablecimiento &&
            //o.Ciudad == vm.Ciudad);
            //if (obj1 != null)
            //{
            //    resultado.Agregar("El nombre del establecimiento existe para la misma ciudad.");
            //}

            return valid;
        }

    }
}
