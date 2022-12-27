using _0TestWebAPI1.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Models
{
    public class Usuario1
    {
        /*private PruebasDbContext _dbContext { get; set; }*/
        
        // public Guid UId { get; set; }
        [Key]
        public string Ci { get; set; }
        public string Password { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; } 
        // public string Ci { get; set; }

        public Guid SexoUId { get; set; }
        [JsonIgnore]
        public Sexo2 Sexo { get; set; }

        // public int Edad { get; set; }
        public Guid GrupoEtarioUId { get; set; }
        /*[NotMapped]
        public string GrupoEtarioNombre { get; set; }*/

        [JsonIgnore]
        public GrupoEtario GrupoEtario { get; set; }

        // public string RolNombre { get; set; }
        /*[JsonIgnore]
        public Rol7 Rol7 { get; set; }*/

        public Guid EscolaridadUId { get; set; }  // se valida en el front y en el controller 
        [JsonIgnore]
        public Escolaridad Escolaridad { get; set; }
       /* public Guid Centro4Id { get; set; }
        [JsonIgnore]
        public Centro4 Centro4 { get; set; }*/

    }
}
