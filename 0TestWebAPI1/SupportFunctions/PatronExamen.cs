using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.SupportFunctions
    {

    // 
    public class PatronExamen // O TestResult O mejor ExamData
        {
        private string patronAsString = "";
        private string respuestaUsuarioAsString = "";

        /*private List<string> imgs = new List<string>() {
            "i1", "i2", "i3", "i4", "i5", "i6", "i7", "i8", "i9", "i10", "i11", "i12", "i13", "i14", "i15", "i16", "i17", "i18", "i19", "i20",
    "i21", "i22", "i23", "i24", "i25", "i26", "i27", "i28", "i29", "i30", "i31", "i32", "i33", "i34", "i35", "i36", "i37", "i38", "i39", "i40",
    "i41", "i42", "i43", "i44", "i45", "i46", "i47", "i48", "i49", "i50", "i51", "i52", "i53", "i54", "i55", "i56", "i57", "i58", "i59", "i60"};
        private List<int> resps = new List<int>() {
            3, 2, 2, 2,3, 3, 2, 3,1, 1, 1, 3,2, 1, 2, 2,2, 2, 3, 1,2, 2, 1, 1,1, 1, 1, 3,1, 3, 2, 2,
    1, 1, 2, 3,1, 1, 2, 3,2, 2, 3, 3,3, 2, 2, 1,1, 1, 1, 2,2, 3, 2, 3,3, 1, 2, 1 };*/


        // Para crear nuevo examen (este string patronOriginal viene de la pruebaMatriz ejemplo pruebaCaritas tiene un patron original y asi)
        public PatronExamen(string patronOriginal)
            {
            patronAsString = patronOriginal;
            }
        // Para revisar examen desde la bd
        public PatronExamen(string patronClave, string respuestaUsuario)
            {
            patronAsString = patronClave;
            respuestaUsuarioAsString = respuestaUsuario;
            }
        /*// Para recibir examen del front
        public PatronExamen(List<string> respuestaDeUsuario)
            {
           // 
            }*/
        public string[] RevisarExamen()
            {
            List<string> keyPattern = ConvertStringToList(patronAsString);
            List<string> userResult = ConvertStringToList(respuestaUsuarioAsString);

            string[] examResultRaw = new string[keyPattern.Count];

            // Con esto reviso de atras para alante, util para hallar el index del ultimo error/acierto y luego sacar las omisiones
            int lastAnswerIndex = 0;
            for (int i = userResult.Count - 1; i > -1; i--)
                {
                // Console.WriteLine(userResult[i] +" "+ i);



                // Si la respuesta del usuario no coincide con la clave
                if (userResult[i] != keyPattern[i])
                    {
                    // Console.WriteLine(int.Parse(userResult[i].Split()[1]));
                    // Console.WriteLine(i < lastAnswerIndex);
                    Console.WriteLine(i + " " + lastAnswerIndex);
                    if (int.Parse(userResult[i].Split()[1]) == 0 && i < lastAnswerIndex)
                        examResultRaw[i] = "omission";

                    // Si esta respuesta fue la ultima respuesta correcta o error se actualiza este index q permite diferenciar luego las omisiones de 
                    // las imagenes no evaluadas
                    else
                        {
                        examResultRaw[i] = "error";

                        if (lastAnswerIndex < i)
                            lastAnswerIndex = i;
                        }
                    }
                // Si no dio una respuesta puede ser omision o q no le dio tiempo

                else if (int.Parse(userResult[i].Split()[1]) == 0)
                    {
                    // Si esta falta de respuesta ocurre antes de la ultima respuesta correcta o error es una omision
                    if (lastAnswerIndex > i)
                        examResultRaw[i] = "omission";
                    // Si esta falta de respuesta ocurre despues de la ultima respuesta correcta o error es q no le dio tiempo
                    else
                        examResultRaw[i] = "notEvaluated";
                    }
                else
                    {
                    // Si la respuesta del usuario coincide con la clave es una anotacion
                    if (userResult[i] == keyPattern[i])
                        {
                        // Si esta respuesta fue la ultima respuesta correcta o error se actualiza este index q permite diferenciar luego las omisiones de 
                        // las imagenes no evaluadas
                        if (lastAnswerIndex < i)
                            { lastAnswerIndex = i; }


                        examResultRaw[i] = "annotation";
                        }

                    }
                }
            return examResultRaw;
            }

        /*// Innecesario probablemente
        void Imagenes()
            {
            for (int i = 0; i < 60; i++)
                {
                imgs.Add("i" + (i + 1));
                };
            }

        // Innecesario probablemente
        public List<string> PatronCaritasOriginal()
            {
            Imagenes();
            List<string> patronOriginal = new List<string>();
            for (int i = 0; i < 60; i++)
                {
                patronOriginal.Add(imgs[i] + " " + resps[i].ToString());
                }
            return patronOriginal;
            }*/

        public string GenerarPatron()
            {
            List<string> patronOriginal = ConvertStringToList(patronAsString);

            List<string> patronRandom = new List<string>();
            Random random = new Random();

            /* REMOVING RANDOMLY ONE BY ONE ELEMENTS FROM PATRONORIGINAL LIST AND ADDING THEM TO PATRONRANDOM LIST */
            while (patronOriginal.Count > 0)
                {
                int randomIndex = random.Next(patronOriginal.Count);
                string imgRespObject = patronOriginal[randomIndex];

                patronRandom.Add(imgRespObject);
                patronOriginal.Remove(imgRespObject);
                }

            return ConvertToString(patronRandom);

            }

        public string ConvertToString(List<string> respuestaDeUsuario)
            {
            string respuestaUsuarioAsString = "";
            foreach (var imgRespObject in respuestaDeUsuario)
                {
                respuestaUsuarioAsString += imgRespObject + ",";
                }
            return respuestaUsuarioAsString;
            }

        public List<string> ConvertStringToList(string respuestaUsuarioAsString)
            {
            List<string> pruebaResult = new List<string>();
            string[] respuestaUsuario = respuestaUsuarioAsString.Split(',');
            foreach (var imgRespObject in respuestaUsuario)
                {

                pruebaResult.Add(imgRespObject);
                }
            pruebaResult.RemoveAt(pruebaResult.Count - 1);

            return pruebaResult;
            }

        }

    }

