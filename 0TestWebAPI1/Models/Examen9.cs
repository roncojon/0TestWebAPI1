using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _0TestWebAPI1.Models
{
    public class Examen9
    {
        [Key]
        public Guid UId { get; set; }
        /*        public string Prueba { get; set; }
                public int MyProperty { get; set; }*/
        /*yyyy-MM-dd'T'HH:mm:ss*/
        public string PruebaMatrizNombre { get; set; }
        [JsonIgnore]
        public Test PruebaMatriz { get; set; }

        /*public long FechaTimeStamp { get; set; }
        [JsonIgnore]
        public Fecha Fecha { get; set; }*/

        public string PatronClave { get; set; }
      
        
        // public bool Activo { get; set; }
    }
}
