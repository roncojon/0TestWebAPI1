using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _0TestWebAPI1.Models
{
    public class UsuarioExamen10
    {
        public Guid UsuarioId { get; set; }

        /* [NotMapped]*/
        /*[NotMapped]
        [JsonIgnore]*/
        public long FechaTimeStamp { get; set; }
        [JsonIgnore]
        public Fecha Fecha { get; set; }


        [JsonIgnore]
        public Usuario1 Usuario { get; set; }
        public Guid ExamenId { get; set; }

        [JsonIgnore]
        public Examen9 Examen { get; set; }

        public string PatronUsuario { get; set; }

        // public int TiempoSegundos { get; set; }
    }
}
