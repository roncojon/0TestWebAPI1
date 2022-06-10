using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _0TestWebAPI1.Models
{
    public class SujetoCentro
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SujetoId"), Required]
        public Sujeto Sujeto { get; set; }

        [ForeignKey("CentroId"), Required]
        public Centro Centro { get; set; } 
    }
}
