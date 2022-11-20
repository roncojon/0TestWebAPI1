using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _0TestWebAPI1.Models
{
    public class Sexo2
    {
        [Key]
        public Guid UId { get; set; }
        public string Nombre { get; set; }
        // public string Acronimo { get; set; }
        public ICollection<Usuario1> Usuarios { get; set; }

    }

}
