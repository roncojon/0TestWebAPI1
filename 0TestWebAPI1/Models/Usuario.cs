using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public long Ci { get; set; }
        public string Password { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; } 
        public string NickName { get; set; }

        public bool Sexo { get; set; }  //true es femeneino

        public int Edad { get; set; }
        public int RolId { get; set; }
        public int GrupoEtarioId { get; set; } // se valida en el controller

        public int EscolaridadId { get; set; }  // se valida en el front y en el controller 

        public ICollection<Centro> Centros { get; set; }
        public ICollection<PruebaBase> PruebasBase { get; set; }
    }
}
