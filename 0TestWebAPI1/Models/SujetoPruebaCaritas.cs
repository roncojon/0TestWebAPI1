using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Models
{
    public class SujetoPruebaCaritas
    {
        [Key]
        public int Id { get; set; } 

        //[ForeignKey("SujetoId"),Required]
        public Sujeto Sujeto { get; set; } 

        //[ForeignKey("PruebaCaritasId"), Required]
        public PruebaDeCaritas PruebaCaritas { get; set; } 
    }
}
