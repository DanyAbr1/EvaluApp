using ApiMysql.Models.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMysql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : Controller
    {
        private SistemacarrosContext _context;

        public VehiculosController(SistemacarrosContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var vehiculos = _context.Vehiculo.Where(v => v.Idusuario1 == id).ToList();
            if (vehiculos == null)
            {
                return BadRequest("No se encontraron datos para este usuario");
            }                      
            return Ok(vehiculos);
        }
    }
}
