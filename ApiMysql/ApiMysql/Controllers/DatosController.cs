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
    public class DatosController:Controller
    {
        private readonly SistemacarrosContext _context;

        public DatosController(SistemacarrosContext context)
        {
            _context = context;
        }

        [HttpPost("buscarDatos")]
        public ActionResult<IEnumerable<Eventos>> BuscarDatos([FromBody] Datos datos)
        {

            var fechahoy = DateTime.Now.Date.ToString("yyyy-MM-dd");
            var result=  _context.Datos.Where(e => e.Idusuario == datos.Idusuario && e.Idvehiculo == datos.Idvehiculo && e.Fecha == DateTime.Parse(fechahoy)).ToList();

            if (result == null)
            {
                return BadRequest("No se encontraron datos para este usuario");
            }
            
            var eventos = CreaEventos(result);
            return Ok(eventos);
        }

        private List<Eventos> CreaEventos(List<Datos> datos)
        {
            var evento = new List<Eventos>();

            var borra = _context.Eventos.Where(e => e.Idusuario == datos[0].Idusuario && e.Idvehiculo == datos[0].Idvehiculo);
            if (datos.Count > 0)
            {
                _context.RemoveRange(borra);
                _context.SaveChanges();
            }
            for (int i = 0; i < datos.Count; i++)
            {
                if (float.Parse(datos[i].Gforce) > 60)
                {

                    evento.Add(new Eventos()
                    {

                        Idvehiculo = datos[i].Idvehiculo,
                        Idusuario = datos[i].Idusuario,
                        Idtipoevento = 1,
                        Puntos = "30"
                    });
                    
                }

                if (float.Parse(datos[i].Velocidad) > 1.5)
                {

                    evento.Add(new Eventos()
                    {

                        Idvehiculo = datos[i].Idvehiculo,
                        Idusuario = datos[i].Idusuario,
                        Idtipoevento = 2,
                        Puntos = "20"
                    });

                }
            }
            
            if (evento != null)
            {
                _context.Eventos.AddRange(evento);
                _context.SaveChanges();
            }

            return evento;
        }
    }
}
