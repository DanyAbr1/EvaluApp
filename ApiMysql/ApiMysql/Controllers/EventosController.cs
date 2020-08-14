using ApiMysql.Models.DBModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMysql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : Controller
    {
        private readonly SistemacarrosContext _context;

        public EventosController(SistemacarrosContext context)
        {
            _context = context;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var eventos = _context.Eventos.Where(e => e.Idusuario == id).ToList();
            if (eventos == null)
            {
                return BadRequest("No se encontraron datos para este usuario");
            }
            return Ok(eventos);
        }
    }
}
