using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _0TestWebAPI1.Models
{
    public class UsuarioRol6
    {
        public Guid UsuarioId { get; set; }
        /*[NotMapped]*/
        public Usuario1 Usuario{ get; set; }
        public string RolNombre { get; set; }
        /*[NotMapped]*/
        public Rol7 Rol { get; set; }
    }
}
