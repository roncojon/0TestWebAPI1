using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Models
{
    public class Centro4 //de trabajo o estudio
    {
        // public int Id { get; set; }
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Usuario1> Usuarios { get; set; }


    }
}
