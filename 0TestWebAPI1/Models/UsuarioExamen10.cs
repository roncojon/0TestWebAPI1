using System;
using System.Text.Json.Serialization;

namespace _0TestWebAPI1.Models
{
    public class UsuarioExamen10
    {
        public Guid Usuario1Id { get; set; }

        [JsonIgnore]
        public Usuario1 Usuario1 { get; set; }
        public Guid Examen9Id { get; set; }

        [JsonIgnore]
        public Examen9 Examen9 { get; set; }

        public string PatronUsuario { get; set; }

        public int TiempoSegundos { get; set; }
    }
}
