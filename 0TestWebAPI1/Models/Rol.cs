﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string NombreDelRol { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}