﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Models
{
    public class UsuarioCentro
    {
        [Key]
        public int Id { get; set; }

        public Usuario Usuario { get; set; }

        public Centro Centro { get; set; } 

    }
}
