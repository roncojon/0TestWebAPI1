using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _0TestWebAPI1.Models
{
    public class UsuarioExamen10
    {
        public Guid Usuario1Id { get; set; }

       /* [NotMapped]*/
        
        public DateTime FechaValue { get; set; }
        [JsonIgnore]

        public Fecha Fecha { get; set; }

        [JsonIgnore]
        public Usuario1 Usuario1 { get; set; }
        public Guid Examen9Id { get; set; }

        [JsonIgnore]
        public Examen9 Examen9 { get; set; }

        public string PatronUsuario { get; set; }

        // public int TiempoSegundos { get; set; }
    }
}
