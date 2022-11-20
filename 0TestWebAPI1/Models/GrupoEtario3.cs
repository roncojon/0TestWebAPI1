using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _0TestWebAPI1.Models
{

    public class GrupoEtario
    {
        [Key]
        public Guid UId { get; set; }
        public string Nombre { get; set; }
        // public int Nivel { get; set; }

        //[Range(1,3)]
        //public int Grupo { get; set; }
        public int EdadMinima { get; set; }

        public int EdadMaxima{ get; set; }
        public ICollection<Usuario1> Usuarios { get; set; }

        
    }
}
