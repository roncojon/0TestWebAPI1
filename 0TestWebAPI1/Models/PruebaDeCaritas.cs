using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.Models
{
    public class PruebaDeCaritas: PruebaBase
    {
        //[Key]
        //public int Id { get; set; } 

        //public DateTime Fecha { get; set; }
        //public int Filas { get; set; }

        //public int Intentos { get; set; }

        //public int Anotaciones { get; set; } 

        //public int Errores { get; set; } 

        //public int Omisiones { get; set; } 

        public int Tipo = 1;

        public PruebaDeCaritas Evaluar(PruebaDeCaritas pc) {
            PruebaDeCaritas npc = pc;
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
            if (pc.IntentosTotales==0)
            {
                npc.EficaciaAtencional = 0;
            }
            else
            {
                npc.EficaciaAtencional = Math.Round(((double)pc.AnotacionesTotales / (double)pc.IntentosTotales) * 100, 2);

            }
            npc.EficienciaAtencional =Math.Round( ((double)pc.AnotacionesTotales / 3.0),2)  ;
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
