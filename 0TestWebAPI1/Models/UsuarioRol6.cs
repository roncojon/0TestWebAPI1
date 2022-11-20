using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _0TestWebAPI1.Models
{
    public class UsuarioRol6
    {
        // public Guid UsuarioCi { get; set; }
        /*[NotMapped]*/
        
        public string UsuarioCi { get; set; }
        public Usuario1 Usuario{ get; set; }
        public Guid RolUId { get; set; }
        /*[NotMapped]*/
        public Rol7 Rol { get; set; }
    }
}
