using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _0TestWebAPI1.Models
{
    public class Escolaridad
    {
        // public Guid Id { get; set; }

        //[Range(1, 3)]
        //public int NivelEscolar { get; set; }

        //public List<Sujeto> Sujetos { get; set; }
        [Key]
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public ICollection<Usuario1> Usuarios { get; set; }
    }
}
