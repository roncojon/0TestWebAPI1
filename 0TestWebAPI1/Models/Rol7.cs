using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Models
{
    public class Rol7
    {
        [Key]
        public Guid UId { get; set; }
        public string Nombre { get; set; }
        // public ICollection<Usuario1> Usuarios { get; set; }
    }
}
