using System;

namespace _0TestWebAPI1.Models
{
    public class Examen9
    {
        public Guid Id { get; set; }
        /*        public string Prueba { get; set; }
                public int MyProperty { get; set; }*/
        /*yyyy-MM-dd'T'HH:mm:ss*/
        public string PruebaMatrizNombre { get; set; }
        public PruebaMatriz8 PruebaMatriz { get; set; }
        public DateTime Fecha { get; set; }
    }
}
