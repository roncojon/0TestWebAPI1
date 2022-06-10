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

     
        public string Nombre { get; set; }

     
        public string Apellidos { get; set; } 

        
        public bool Sexo { get; set; }  //true es femeneino

       
        public int Edad { get; set; }

        public GrupoEtario GrupoEtario { get; set; } // se valida en el controller

        public Escolaridad Escolaridad { get; set; }  // se valida en el front y en el controller 
    }
}
