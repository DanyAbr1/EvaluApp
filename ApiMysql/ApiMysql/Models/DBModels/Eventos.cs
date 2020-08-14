using System;
using System.Collections.Generic;

namespace ApiMysql.Models.DBModels
{
    public partial class Eventos
    {
        public int Ideventos { get; set; }
        public int Idtipoevento { get; set; }
        public string Puntos { get; set; }
        public int Idvehiculo { get; set; }
        public int Idusuario { get; set; }

        //public virtual Tipoeventos IdtipoeventoNavigation { get; set; }
        //public virtual Usuario IdusuarioNavigation { get; set; }
        //public virtual Vehiculo IdvehiculoNavigation { get; set; }
    }
}
