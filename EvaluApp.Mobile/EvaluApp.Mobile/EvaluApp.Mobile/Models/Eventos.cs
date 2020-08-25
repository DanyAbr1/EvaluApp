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
                    Descripcion = "Limite de exceso de velocidad.";
                    
                }
                else if ((value == 2))
                {
                    Descripcion = "Accidente.";
                }
                else
                {
                    Descripcion = "Compra de articulos.";
                }

                _idtipoevento = value;
            }
        }        
        public float Velocidad { get; set; }
        public float VelocidadMaxima { get; set; }
        public string Puntos { get; set; }
        public int Idvehiculo { get; set; }
        public int Idusuario { get; set; }
        public string Descripcion { get; set; }
        public string Hora { get; set; }
    }
}
