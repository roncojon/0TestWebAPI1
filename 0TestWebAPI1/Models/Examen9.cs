﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _0TestWebAPI1.Models
{
    public class Examen9
    {
        [Key]
        public Guid UId { get; set; }
        /*        public string Prueba { get; set; }
                public int MyProperty { get; set; }*/
        /*yyyy-MM-dd'T'HH:mm:ss*/
        public Guid TestUId { get; set; }
        [JsonIgnore]
        public Test Test { get; set; }

        /*public long Fecha { get; set; }
        [JsonIgnore]
        public Fecha Fecha { get; set; }*/

        public string PatronClave { get; set; }

        public long FechaInicio { get; set; }
        public long FechaFin { get; set; }

        // public bool Activo { get; set; }
        }
}
