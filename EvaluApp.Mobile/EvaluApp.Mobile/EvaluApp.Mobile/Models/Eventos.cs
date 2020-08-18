namespace EvaluApp.Mobile.Models
{
    public class Eventos
    {
        private int _idtipoevento;

        public int Ideventos { get; set; }
        public int Idtipoevento
        {
            get => _idtipoevento;
            set
            {
                if ((value == 1))
                {
                    Descripcion = "Ascidente.";
                }
                else if ((value == 2))
                {
                    Descripcion = "Limite de exceso de velocidad.";
                }
                else
                {
                    Descripcion = "Compra de articulos.";
                }
            }
        }
        public string Puntos { get; set; }
        public int Idvehiculo { get; set; }
        public int Idusuario { get; set; }
        public string Descripcion { get; set; }
    }
}
