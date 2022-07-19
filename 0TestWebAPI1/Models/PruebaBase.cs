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

        //[DataFormatString("{0:dd-MM-yyyy}")]

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy")]
        public DateTime Fecha { get; set; }
        public int Filas { get; set; }

        public int Intentos { get; set; }

        public int Anotaciones { get; set; }

        public int Errores { get; set; }

        public int Omisiones { get; set; }

        public double IGAP { get; set; }  //se valida en el controller

        public double ICI { get; set; }  //se valida en el controller

        public double PorCientoDeAciertos { get; set; }//se valida en el controller, preferiria q fuera en el front

        public double EficaciaAtencional { get; set; }  //se valida en el controller, preferiria q fuera en el front

        public double EficienciaAtencional { get; set; } //se valida en el controller, preferiria q fuera en el front

        public double RendimientoAtencional { get; set; }  //se valida en el controller, preferiria q fuera en el front

        public double CalidadDeLaAtencion { get; set; }  //se valida en el controller, preferiria q fuera en el front

        public double DatosAtencion { get; set; }  //se valida en el controller, preferiria q fuera en el front
        //public int UsuarioId { get; set; }
        //public Usuario Usuario { get; set; }
    }
}
