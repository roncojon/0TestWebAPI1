using _0TestWebAPI1.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Models
{
    public class Usuario1
    {
        /*private PruebasDbContext _dbContext { get; set; }*/
        [Key]
        public Guid Id { get; set; }

        public ulong Ci { get; set; }
        public string Password { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; } 
        public string NickName { get; set; }

        public string SexoNombre { get; set; }
        // public Sexo2 Sexo2 { get; set; }  //true es femeneino

        public int Edad { get; set; }
        public string RolNombre { get; set; }
        public string GrupoEtarioNombre { get; set; } // se valida en el controller

        public string EscolaridadNombre { get; set; }  // se valida en el front y en el controller 

        //public ICollection<Centro> Centros { get; set; }
        // public ICollection<PruebaMatriz8> PruebaMatriz8 { get; set; }

       /* public List<PruebaDeCaritas> PcConFilas(List<PruebaDeCaritas> pruebasCaritas)
        {
            List<PruebaDeCaritas> pC = new List<PruebaDeCaritas>();
            foreach (var pc in pruebasCaritas)
            {
                List<Fila> filas = new List<Fila>();
                foreach (var fila in _dbContext.Fila)
                {
                    if (fila.PruebaBaseId == pc.Id)
                    {
                        filas.Add(fila);
                    }
                }
                pc.Filas = filas;
                pC.Add(pc);
            }
            return pC;
        }*/

      
    }
}
