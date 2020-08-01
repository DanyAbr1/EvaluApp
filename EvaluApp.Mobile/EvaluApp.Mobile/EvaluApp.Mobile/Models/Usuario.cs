using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluApp.Mobile.Models
{
    public class Usuario
    {
        public int Idusuario { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Cedula { get; set; }
        public string Apellido2 { get; set; }
        public DateTime Fechanacimiento { get; set; }
        public string Contrasena { get; set; }
    }
}
