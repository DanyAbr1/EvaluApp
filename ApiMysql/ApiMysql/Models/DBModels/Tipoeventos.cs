using System;
using System.Collections.Generic;

namespace ApiMysql.Models.DBModels
{
    public partial class Tipoeventos
    {
        public Tipoeventos()
        {
            Eventos = new HashSet<Eventos>();
        }

        public int Idtipoeventos { get; set; }
        public string Tipoeventos1 { get; set; }

        public virtual ICollection<Eventos> Eventos { get; set; }
    }
}
