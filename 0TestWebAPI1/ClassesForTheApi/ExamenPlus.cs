using _0TestWebAPI1.Models;
using System;

namespace _0TestWebAPI1.ClassesForTheApi
    {
    public class ExamenPlus
        {
        public Guid Id { get; set; }
        /*        public string Prueba { get; set; }
                public int MyProperty { get; set; }*/
        /*yyyy-MM-dd'T'HH:mm:ss*/
        public string PruebaMatrizNombre { get; set; }
        public string PatronClave { get; set; }
        public DateTime Fecha { get; set; }

        public string Descripcion { get; set; }
        public int CantColumnas { get; set; }
        public int CantidadFilas { get; set; }
        public int TiempoLimiteMs { get; set; }
        }
    }
