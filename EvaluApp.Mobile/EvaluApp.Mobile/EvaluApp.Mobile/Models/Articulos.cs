using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluApp.Mobile.Models
{
    public class Articulos
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }
        public float TotalItems { get; set; }
        public int Cantidad { get; set; }
        public string Matricula { get; set; }
    }
}
