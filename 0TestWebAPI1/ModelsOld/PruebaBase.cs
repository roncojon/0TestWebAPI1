using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _0TestWebAPI1.Models
{
    public class PruebaCaritas
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        //[DataFormatString("{0:dd-MM-yyyy}")]

        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy")]
        public string Fecha { get; set; }
        public ICollection<Fila> Filas { get; set; }

        public int IntentosTotales { get; set; }

        public int AnotacionesTotales { get; set; }

        public int ErroresTotales { get; set; }

        public int OmisionesTotales { get; set; }

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
        public int Tipo = 1;

        public PruebaCaritas Evaluar(PruebaCaritas pc)
        {
            PruebaCaritas npc = pc;
            npc.Filas = new List<Fila>();

            npc.IGAP = pc.AnotacionesTotales - (pc.ErroresTotales + pc.OmisionesTotales);
            if (pc.AnotacionesTotales + pc.ErroresTotales + pc.OmisionesTotales == 0)
            {
                npc.ICI = 0;
            }
            else
            {
                npc.ICI = Math.Round(((double)pc.AnotacionesTotales - ((double)pc.ErroresTotales + (double)pc.OmisionesTotales)) / ((double)pc.AnotacionesTotales + ((double)pc.ErroresTotales + (double)pc.OmisionesTotales)) * 100.0, 2);

            }

            npc.PorCientoDeAciertos = Math.Round(((double)pc.AnotacionesTotales - (double)pc.ErroresTotales) / 60.0 * 100.0, 2);
            if (pc.IntentosTotales == 0)
            {
                npc.EficaciaAtencional = 0;
            }
            else
            {
                npc.EficaciaAtencional = Math.Round(((double)pc.AnotacionesTotales / (double)pc.IntentosTotales) * 100, 2);

            }
            npc.EficienciaAtencional = Math.Round(((double)pc.AnotacionesTotales / 3.0), 2);
            npc.RendimientoAtencional = Math.Round((double)pc.EficaciaAtencional / 3.0, 2);
            if (pc.AnotacionesTotales + pc.OmisionesTotales == 0)
            {
                npc.CalidadDeLaAtencion = 0;
            }
            else
            {
                npc.CalidadDeLaAtencion = Math.Round(((double)pc.AnotacionesTotales - (double)pc.ErroresTotales) / ((double)pc.AnotacionesTotales + (double)pc.OmisionesTotales) * 100.0, 2);

            }

            switch (pc.AnotacionesTotales)
            {
                case <= 23:
                    npc.DatosAtencion = 1;
                    break;
                case >= 24:
                    npc.DatosAtencion = 2;
                    break;
            }


            return npc;
        }
    }
}
