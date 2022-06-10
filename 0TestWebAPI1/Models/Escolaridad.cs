using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _0TestWebAPI1.Models
{
    public class Escolaridad
    {
        public int Id { get; set; }

        [Range(1, 3)]
        public int NivelEscolar { get; set; }
    }
}
