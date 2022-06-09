using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Models
{
    public class Sujeto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "{0} es requerido.")]
        public string Apellidos { get; set; } 

        [Required(ErrorMessage = "{0} es requerido.")]
        public bool Sexo { get; set; }  //true es femeneino

        [Required(ErrorMessage = "{0} es requerido.")]
        public int Edad { get; set; } 

        public int GrupoEtario { get; set; } // se valida en el controller

        public int Escolaridad { get; set; }  // se valida en el front y en el controller 
    }
}
