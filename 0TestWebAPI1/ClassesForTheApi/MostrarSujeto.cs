using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.ClassesForTheApi
{
    public class MostrarSujeto: IEnumerable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int GrupoEtario { get; set; }
        public int Escolaridad { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
