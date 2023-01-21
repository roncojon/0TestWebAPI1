using _0TestWebAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0TestWebAPI1.ClassesForTheApi
    {
    public class UserDataPost
        {
        public UserRegister Usuario { get; set; }

        // public List<string> Roles { get; set; }
        public List<Guid> RolesUIds { get; set; }
        }
    }
