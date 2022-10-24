using System;

namespace _0TestWebAPI1.Models
{
    public class UsuarioExamen10
    {
        public Guid UsuarioId { get; set; }
        public Usuario1 Usuario1 { get; set; }
        public Guid ExamenId { get; set; }
        public Examen9 Examen9 { get; set; }
        public string PatronClave { get; set; }
        public string PatronUsuario { get; set; }
        public int TiempoSegundos { get; set; }
    }
}
