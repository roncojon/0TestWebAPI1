using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace _0TestWebAPI1.Models
    {
    public class Fecha
        {
        [Key]
        public long TimeStamp { get; set; }
        // public ICollection<UsuarioExamen10> UsuariosExamenes { get; set; }
        // public ICollection<Examen9> Examenes { get; set; }
        
        public Fecha()
            {
            DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
           /* Fecha fechaNow = new Fecha();*/
            TimeStamp = dto.ToUnixTimeMilliseconds();
            }
        }
    }
