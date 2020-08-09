using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMysql.Models.DBModels
{
    public class UsuarioDto
    {
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }

        public string Apellido1 { get; set; }

        public string Apellido2 { get; set; }

        public string Contrasena { get; set; }
        public string Tipo { get; set; }

        public string Matricula { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public DateTime Vence { get; set; }

        public string Cedula { get; set; }
    }
}
