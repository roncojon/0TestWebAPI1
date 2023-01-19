using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.ClassesForTheApi
    {
    public class UserRegister
        {
        public string Ci { get; set; }
        public string Password { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public Guid SexoUId { get; set; }
   
        public Guid EscolaridadUId { get; set; } 

        }
    }
