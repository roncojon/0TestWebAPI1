using _0TestWebAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.ClassesForTheApi
{
    public class UsuariosXcentro
    {

        public int CentroId { get; set; }
        public string NombreDelCentro { get; set; }

        public List<Usuario> Usuarios { get; set; }
    }
}
