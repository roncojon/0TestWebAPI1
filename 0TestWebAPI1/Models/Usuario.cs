using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Models
{
    public class Usuario//aaaaaaaaa
    {
        public int Id { get; set; }

        public int Ci { get; set; }

        public int RolId { get; set; }
        public Rol Rol { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public bool Sexo { get; set; }  //true es femeneino 

        public int Edad { get; set; }

        //public int GrupoEtarioId { get; set; } //relacion 

        public int GrupoEtarioId { get; set; }
        //public  GrupoEtario GrupoEtario { get; set; } // propiedad de navegacion 

        //public int EscolaridadId { get; set; } //relacion 
        public int EscolaridadId { get; set; }
        //public  Escolaridad Escolaridad { get; set; }  // se valida en el front y en el controller  
    }
}

//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;

//namespace _0TestWebAPI1.Models
//{
//    public class Sujeto
//    {
//        [Key]
//        public int Id { get; set; }


//        public string Nombre { get; set; }


//        public string Apellidos { get; set; } 


//        public bool Sexo { get; set; }  //true es femeneino


//        public int Edad { get; set; }

//        public int GrupoEtarioId { get; set; }
//        public GrupoEtario GrupoEtario { get; set; } // se valida en el controller

//        //public int GrupoEtarioId { get; set; }

//        public Escolaridad Escolaridad { get; set; }  // se valida en el front y en el controller 
//    }
//}

