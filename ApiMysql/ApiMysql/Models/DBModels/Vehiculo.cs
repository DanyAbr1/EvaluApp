using System;
using System.Collections.Generic;

namespace ApiMysql.Models.DBModels
{
    public partial class Vehiculo
    {
        //public Vehiculo()
        //{
        //    Eventos = new HashSet<Eventos>();
        //}

        public int Idvehiculo { get; set; }
        public string Matricula { get; set; }
        public int Idusuario1 { get; set; }

        public virtual Usuario Idusuario1Navigation { get; set; }
      //  public virtual ICollection<Eventos> Eventos { get; set; }
    }
}
