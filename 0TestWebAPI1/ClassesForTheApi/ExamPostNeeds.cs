using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.ClassesForTheApi
    {
    public class ExamPostNeeds
        {
        public Guid TestUId { get; set; }

        public bool IsPatronOriginal { get; set; }

        public List<string> UsuariosCiList { get; set; }
        public long FechaInicio { get; set; }
        public long FechaFin { get; set; }

        }
    }
