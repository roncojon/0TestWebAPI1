using _0TestWebAPI1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.ClassesForTheApi
{
    public class UserData
    {
        public int Id { get; set; }
        public ulong Ci { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }

        public bool Sexo { get; set; }  //true es femeneino

        public int Edad { get; set; }
        public int RolId { get; set; }
        public int GrupoEtarioId { get; set; } // se valida en el controller

        public List<string> Centros { get; set; } //esto es pal get pa ver los centros a q pertenece 1 usuario

        public List<int> CentrosIds { get; set; } //esto es pal post, pa asignar los centros a los q percenece 1 usuario

        public List<PruebaDeCaritas> PruebaDeCaritas {get; set;}
        public int EscolaridadId { get; set; }  // se valida en el front y en el controller
    }
}
