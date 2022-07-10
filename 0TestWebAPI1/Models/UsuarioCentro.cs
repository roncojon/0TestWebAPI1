using System;
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

        [ForeignKey("SujetoId"), Required]
        public Usuario Sujeto { get; set; }

        [ForeignKey("CentroId"), Required]
        public Centro Centro { get; set; } 
    }
}
