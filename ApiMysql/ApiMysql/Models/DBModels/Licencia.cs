using System;
using System.Collections.Generic;

namespace ApiMysql.Models.DBModels
{
    public partial class Licencia
    {
        public int Idlicencia { get; set; }
        public string Tipo { get; set; }
        public DateTime Vence { get; set; }
        public int Idusuario { get; set; }

        public virtual Usuario IdusuarioNavigation { get; set; }
    }
}
