﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _0TestWebAPI1.Models
{
    public class Escolaridad
    {
        [Key]
        public Guid UId { get; set; }

        //[Range(1, 3)]
        //public int NivelEscolar { get; set; }

        //public List<Sujeto> Sujetos { get; set; }
        
        public string Nombre { get; set; }
        // public int Nivel { get; set; }
        public ICollection<Usuario1> Usuarios { get; set; }
    }
}
