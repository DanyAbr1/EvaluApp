using System;
using System.Collections.Generic;

namespace ApiMysql.Models.DBModels
{
    public partial class Usuario
    {
        public Usuario()
        {
            Eventos = new HashSet<Eventos>();
            Licencia = new HashSet<Licencia>();
            Vehiculo = new HashSet<Vehiculo>();
        }

        public int Idusuario { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Cedula { get; set; }
        public string Apellido2 { get; set; }
        public DateTime Fechanacimiento { get; set; }
        public string Contrasena { get; set; }

        public virtual ICollection<Eventos> Eventos { get; set; }
        public virtual ICollection<Licencia> Licencia { get; set; }
        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
