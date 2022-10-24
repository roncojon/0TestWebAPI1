using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Models
{
    public class Fila
    {
        public int Id { get; set; }
        public int PruebaBaseId { get; set; }
        public int Attempts { get; set; }
        public int Annotations { get; set; }
        public int Errors { get; set; }
        public int Omissions { get; set; }
    }
}
