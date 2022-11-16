using System;
using System.Text.Json.Serialization;

namespace _0TestWebAPI1.Models
{
    public class Examen9
    {
        public Guid Id { get; set; }
        /*        public string Prueba { get; set; }
                public int MyProperty { get; set; }*/
        /*yyyy-MM-dd'T'HH:mm:ss*/
        public string PruebaMatrizNombre { get; set; }
        [JsonIgnore]
        public PruebaMatriz8 PruebaMatriz { get; set; }

        public string PatronClave { get; set; }
        /*public string PatronUsuario { get; set; }*/
        public DateTime Fecha { get; set; }

        public bool Activo { get; set; }
    }
}
