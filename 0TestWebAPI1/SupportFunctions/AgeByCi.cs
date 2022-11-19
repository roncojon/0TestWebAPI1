using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.SupportFunctions
    {
    public class AgeByCi
        {
        public int Get(string Ci, DateTime dateToCompare)
            {
            string ciAsDate = Ci.Substring(0, 6);
            string ciAsDateYear = ciAsDate.Substring(0, 2);

            /////////////////////
            int edad = 0;
            string actualYear = DateTime.Now.Year.ToString();
            if (int.Parse(ciAsDateYear) <= int.Parse(actualYear.Substring(2, 2)))
                {
                ciAsDate = "20" + ciAsDate;
                }
            else
                {
                // int actualYear = DateTime.Now.Year;
                ciAsDate = "19" + ciAsDate;
                }
            ciAsDateYear = ciAsDate.Substring(0, 4);
            // Console.WriteLine(ciAsDateYear);
            string ciAsDateMonth = ciAsDate.Substring(4, 2);
            // Console.WriteLine(ciAsDateMonth);
            string ciAsDateDay = ciAsDate.Substring(6, 2);
            // Console.WriteLine(ciAsDateDay);
            // ciAsDate = ciAsDateYear + ciAsDate.Substring(2, 6);
            ciAsDate = ciAsDateMonth + "/" + ciAsDateDay + "/" + ciAsDateYear;
            string[] ciAsDateArray = { ciAsDateMonth, "/", ciAsDateDay, "/", ciAsDateYear };
            ciAsDate = string.Concat(ciAsDateArray);
            // Console.WriteLine(ciAsDate);

            DateTime userBornDate = DateTime.Parse(ciAsDate);
            // DateTime dateToCompare = DateTime.Now;

            // DateTimeOffset userBornDateMs = new DateTimeOffset(userBornDate);
            // DateTimeOffset actualDateMs = new DateTimeOffset(dateToCompare);


            int now = int.Parse(dateToCompare.ToString("yyyyMMdd"));
            Console.WriteLine(now);
            int dob = int.Parse(userBornDate.ToString("yyyyMMdd"));
            Console.WriteLine(dob);
            int age = (now - dob) / 10000;
            return age;
            }

        }
    }
