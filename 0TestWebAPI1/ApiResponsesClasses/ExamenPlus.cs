using _0TestWebAPI1.Models;
using System;
using System.Collections.Generic;

namespace _0TestWebAPI1.ClassesForTheApi
    {
    public class ExamenPlus
        {
        public Guid Id { get; set; }
        /*        public string Prueba { get; set; }
                public int MyProperty { get; set; }*/
        /*yyyy-MM-dd'T'HH:mm:ss*/
        public string TestNombre { get; set; }
        public Guid TestUId { get; set; }
        public string PatronClave { get; set; }
        public string Descripcion { get; set; }
        // public long Fecha { get; set; }
        public int CantColumnas { get; set; }
        public int CantidadFilas { get; set; }
        public int TiempoLimiteMs { get; set; }

        public long FechaInicio { get; set; }
        public long FechaFin { get; set; }
        public bool EstaActivo { get; set; }
        public bool EsPatronOriginal { get; set; }
        public List<Usuario1> Usuarios { get; set; }

        public List<ResultadoDe1Examen> Results { get; set; }
        // public bool Activo { get; set; }
        }
    }
