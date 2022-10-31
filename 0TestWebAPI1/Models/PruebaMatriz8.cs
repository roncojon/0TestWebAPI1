using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace _0TestWebAPI1.Models
{
    public class PruebaMatriz8
    {
        [Key]
        [Required(ErrorMessage = "Name is required")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CantidadFilas { get; set; }
        public int CantColumnas { get; set; }
        public int TiempoLimiteMs { get; set; }
        /*[IgnoreDataMember]*/
        [JsonIgnore]
        public ICollection<Examen9> Examenes { get; set; }
    }
}
