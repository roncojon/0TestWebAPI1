//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace _0TestWebAPI1.SupportFunctions
//{
//    public class Functions
//    {
//        public PruebaDeCaritas(PruebaDeCaritas pc)
//        {
//            pc.IGAP = pc.Anotaciones - (pc.Errores + pc.Omisiones);
//            pc.ICI = (pc.Anotaciones - (pc.Errores + pc.Omisiones) / pc.Anotaciones + (pc.Errores + pc.Omisiones)) * 100;
//            pc.PorCientoDeAciertos = (pc.Anotaciones - pc.Errores) / 60 * 100;
//            pc.EficaciaAtencional = (pc.Anotaciones / pc.Intentos) * 100;
//            pc.EficienciaAtencional = pc.Anotaciones / 3;
//            pc.RendimientoAtencional = pc.EficaciaAtencional / 3;
//            pc.CalidadDeLaAtencion = (pc.Anotaciones - pc.Errores) / (pc.Anotaciones + pc.Omisiones) * 100;
//            switch (pc.Anotaciones)
//            {
//                case <= 23:
//                    pc.DatosAtencion = 1;
//                    break;
//                case >= 24:
//                    pc.DatosAtencion = 2;
//                    break;
//            }

//            return
//        }
//    }
//}
