using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _0TestWebAPI1.Models
{
    public class PruebaBase
    {
        [Key]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }
        public int Filas { get; set; }

        public int Intentos { get; set; }

        public int Anotaciones { get; set; }

        public int Errores { get; set; }

        public int Omisiones { get; set; }
    }
}
