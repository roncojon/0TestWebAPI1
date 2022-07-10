using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Models
{
    public class Bolon
    {
        public Usuario Sujeto { get; set; }
        public Centro Centro { get; set; }
        public PruebaBase PruebaBase { get; set; }
        public PruebaDeCaritas PruebaDeCaritas { get; set; }

        public Escolaridad Escolaridad { get; set; }
        public GrupoEtario GrupoEtario { get; set; }
    }
}
