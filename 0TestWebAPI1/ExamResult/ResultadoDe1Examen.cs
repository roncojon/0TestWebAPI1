using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using _0TestWebAPI1.SupportFunctions;

namespace _0TestWebAPI1.Models
    {
    public class ResultadoDe1Examen
        {
        [Key]
       // public int Id { get; set; }
        public string UsuarioCi { get; set; }

        //[DataFormatString("{0:dd-MM-yyyy}")]

        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy")]
        public string Fecha { get; set; }
        public List<Fila> Filas { get; set; }

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
        //public int UsuarioCi { get; set; }
        //public Usuario Usuario { get; set; }
        public int Tipo = 1;

        public ResultadoDe1Examen(Test test, string patronRespuestaUsuario, string userCi)
            {
            UsuarioCi = userCi;

            if (patronRespuestaUsuario != null)
                {
                if (test.PatronOriginal.Length == patronRespuestaUsuario.Length)
                    {
                    PatronExamen pattern = new PatronExamen(test.PatronOriginal, patronRespuestaUsuario);
                    string[] resultAsStringList = pattern.RevisarExamen();
                    List<Fila> filasTemp = new List<Fila>();
                    double totalElements = 0;
                    // TEMPS 
                    int attempts = 0;
                    int annotations = 0;
                    int errors = 0;
                    int omissions = 0;
                    // Total Temps
                    int totalAttempts = 0;
                    int totalAnnotations = 0;
                    int totalErrors = 0;
                    int totalOmissions = 0;
                    for (int i = 0; i < resultAsStringList.Count(); i++)
                        {
                        totalElements++;
                        if (resultAsStringList[i] == "omission")
                            {
                            totalOmissions++;
                            omissions++;
                            totalAttempts++;
                            attempts++;
                            }
                        else if (resultAsStringList[i] == "error")
                            {
                            totalErrors++;
                            errors++;
                            totalAttempts++;
                            attempts++;
                            }
                        else if (resultAsStringList[i] == "annotation")
                            {
                            totalAnnotations++;
                            annotations++;
                            totalAttempts++;
                            attempts++;
                            }
                        if ((i+1) % test.CantColumnas == 0)
                            {
                            Fila filaTemp = new Fila();

                            /*int annotationsTemp = annotations;
                            int attemptsTemp = attempts;
                            int errorsTemp = errors;
                            int omissionsTemp = omissions;

                            filaTemp.Annotations = annotationsTemp;
                            filaTemp.Attempts = attemptsTemp;
                            filaTemp.Errors = errorsTemp;
                            filaTemp.Omissions = omissionsTemp;
                            filasTemp.Add(filaTemp);*/

                            filaTemp.Annotations = annotations;
                            filaTemp.Attempts = attempts;
                            filaTemp.Errors = errors;
                            filaTemp.Omissions = omissions;
                            filasTemp.Add(filaTemp);

                            annotations = 0;
                            attempts = 0;
                            errors = 0;
                            omissions = 0;
                            }
                        }
                    Filas = filasTemp;
                    IntentosTotales = totalAttempts;
                    AnotacionesTotales = totalAnnotations;
                    ErroresTotales = totalErrors;
                    OmisionesTotales = totalOmissions;

                    IGAP = AnotacionesTotales - (ErroresTotales + OmisionesTotales);
                    if (AnotacionesTotales + ErroresTotales + OmisionesTotales == 0)
                        {
                        ICI = 0;
                        }
                    else
                        {
                        ICI = Math.Round(((double)AnotacionesTotales - ((double)ErroresTotales + (double)OmisionesTotales)) / ((double)AnotacionesTotales + ((double)ErroresTotales + (double)OmisionesTotales)) * 100.0, 2);
                        }
                    PorCientoDeAciertos = Math.Round(((double)AnotacionesTotales - (double)ErroresTotales) / totalElements/*60.0*/ * 100.0, 2);
                    if (IntentosTotales == 0)
                        {
                        EficaciaAtencional = 0;
                        }
                    else
                        {
                        EficaciaAtencional = Math.Round(((double)AnotacionesTotales / (double)IntentosTotales) * 100, 2);
                        }
                    EficienciaAtencional = Math.Round(((double)AnotacionesTotales / 3.0), 2);
                    RendimientoAtencional = Math.Round((double)EficaciaAtencional / 3.0, 2);
                    if (AnotacionesTotales + OmisionesTotales == 0)
                        {
                        CalidadDeLaAtencion = 0;
                        }
                    else
                        {
                        CalidadDeLaAtencion = Math.Round(((double)AnotacionesTotales - (double)ErroresTotales) / ((double)AnotacionesTotales + (double)OmisionesTotales) * 100.0, 2);
                        }

                    switch (AnotacionesTotales)
                        {
                        case <= 23:
                            DatosAtencion = 1;
                            break;
                        case >= 24:
                            DatosAtencion = 2;
                            break;
                        }
                    }
                }
            }
        /* public ResultadoDe1Examen Evaluar(ResultadoDe1Examen pc)
             {
             ResultadoDe1Examen npc = pc;
             Filas = new List<Fila>();

             IGAP = AnotacionesTotales - (ErroresTotales + OmisionesTotales);
             if (AnotacionesTotales + ErroresTotales + OmisionesTotales == 0)
                 {
                 ICI = 0;
                 }
             else
                 {
                 ICI = Math.Round(((double)AnotacionesTotales - ((double)ErroresTotales + (double)OmisionesTotales)) / ((double)AnotacionesTotales + ((double)ErroresTotales + (double)OmisionesTotales)) * 100.0, 2);
                 }

             PorCientoDeAciertos = Math.Round(((double)AnotacionesTotales - (double)ErroresTotales) / 60.0 * 100.0, 2);
             if (IntentosTotales == 0)
                 {
                 EficaciaAtencional = 0;
                 }
             else
                 {
                 EficaciaAtencional = Math.Round(((double)AnotacionesTotales / (double)IntentosTotales) * 100, 2);
                 }
             EficienciaAtencional = Math.Round(((double)AnotacionesTotales / 3.0), 2);
             RendimientoAtencional = Math.Round((double)EficaciaAtencional / 3.0, 2);
             if (AnotacionesTotales + OmisionesTotales == 0)
                 {
                 CalidadDeLaAtencion = 0;
                 }
             else
                 {
                 CalidadDeLaAtencion = Math.Round(((double)AnotacionesTotales - (double)ErroresTotales) / ((double)AnotacionesTotales + (double)OmisionesTotales) * 100.0, 2);
                 }

             switch (AnotacionesTotales)
                 {
                 case <= 23:
                     DatosAtencion = 1;
                     break;
                 case >= 24:
                     DatosAtencion = 2;
                     break;
                 }

             return npc;
             }*/
        }
    }
