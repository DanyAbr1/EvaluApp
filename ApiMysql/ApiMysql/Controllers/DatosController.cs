﻿using ApiMysql.Models.DBModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace ApiMysql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosController : Controller
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
            if (datos.Fecha != null) fechahoy = datos.Fecha.ToString("yyyy-MM-dd");
            var result = _context.Datos.Where(e => e.Idusuario == datos.Idusuario && e.Idvehiculo == datos.Idvehiculo && e.Fecha == DateTime.Parse(fechahoy)).ToList();
            result = result.Where(e => float.Parse(e.Velocidad) > 15).ToList();
            if (result == null )
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

                var speed = SpeedMax(datos[i].Latitud, datos[i].Longitud);


                if ((speed > 0) && speed < float.Parse(datos[i].Velocidad))
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

        private float SpeedMax(string lat, string lon)
        {
            //lat = "19.4463081";
            //lon = "-70.6849884";
            string html = string.Empty;
            string url = @"http://" + $"overpass-api.de/api/interpreter?data=[out:json];way (around:70, {lat},{lon}) [maxspeed];out;";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                var Sustituir = html.ToString().Replace("maxspeed", "@maxspeed");

                var Texto = Sustituir.Split('@');
                var Texto2 = Texto[1].ToString();
                var Sustituir2 = Texto2.Split(',');
                var valores = Sustituir2[0].ToString().Split(':');
                var Resultado = valores[1].ToString();
                return float.Parse(Resultado.Replace("\"", ""));
            }
            catch (Exception)
            {
                return 60;
            }
        }

    }
}
