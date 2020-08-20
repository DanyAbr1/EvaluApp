using ApiMysql.Models.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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


        [HttpPost("AddVehiculo")]
        public IActionResult Post([FromBody] Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {                
                var result = _context.Vehiculo.FirstOrDefault(v => v.Matricula == vehiculo.Matricula );
                if (result != null)
                {
                    return BadRequest("La Matricula ya esta registrada.");
                }

                var vehiculoObj = new Vehiculo()
                {
                    Matricula = vehiculo.Matricula,
                    Idusuario1 = vehiculo.Idusuario1
                };

                _context.Vehiculo.Update(vehiculoObj);
                _context.SaveChanges();                
            }
            catch (Exception)
            {
                return StatusCode(500, "No se pudo completar la operación.");
            }

            return NoContent();
        }
    }
}
