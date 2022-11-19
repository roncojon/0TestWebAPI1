using _0TestWebAPI1.ClassesForTheApi;
using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
using _0TestWebAPI1.SupportFunctions;
using AuthenticationPlugin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _0TestWebAPI1.Controllers
    {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerSuper<Usuario1, string>
        {
        private readonly IConfiguration _configuration; /* readonly */
        private readonly AuthService _auth;
        // private readonly PruebasDbContext _dbContext;   /* readonly */

        public UsuarioController(PruebasDbContext context, IConfiguration configuration) : base(context)
            {
            _configuration = configuration;
            _auth = new AuthService(_configuration);
            /* _dbContext = dbContext;*/
            }
        [HttpGet]
        // [Authorize]
        [Route("usuariosplus")]
        public async Task<List<UserData>> GetAllPlus()
            {
            List<Usuario1> usuarios = await _dbContext.Set<Usuario1>().ToListAsync();
            List<UserData> usuariosPlus = new List<UserData>();

            foreach (var user in usuarios)
                {
                UserData udTemp = new UserData();
                udTemp.Ci = user.Ci;
                udTemp.Nombre = user.Nombre;
                udTemp.Apellidos = user.Apellidos;
                udTemp.UserName = user.UserName;
                udTemp.Password = user.Password;
                udTemp.EscolaridadNombre = user.EscolaridadNombre;
                udTemp.GrupoEtarioNombre = user.GrupoEtarioNombre;
                List<string> roles = new List<string>();
                foreach (var usuarioRol in _dbContext.UsuarioRol)
                    {
                    if (usuarioRol.UsuarioCi == user.Ci)
                        roles.Add(usuarioRol.RolNombre);
                    }
                udTemp.Roles = roles;
                AgeByCi age = new AgeByCi();
                DateTime actualDate = DateTime.Now;
                udTemp.Edad =age.Get(user.Ci, actualDate);
                udTemp.SexoNombre = user.SexoNombre;

                usuariosPlus.Add(udTemp);
                }
            return usuariosPlus;
            }
        [NonAction]
        public override async Task<Usuario1> GetById(string id)  // public async Task<ActionResult<T>> GetById(Z id)
            { return new Usuario1(); }
        // [Route("getbyci")]
        [HttpGet("{ci}")]
        public  async Task<UserData> GetByCi(string ci)  // public async Task<ActionResult<T>> GetById(Z id)
            {

            Usuario1 user= await _dbContext.FindAsync<Usuario1>(ci);

            UserData udTemp = new UserData();
            udTemp.Ci = user.Ci;
            udTemp.Nombre = user.Nombre;
            udTemp.Apellidos = user.Apellidos;
            udTemp.UserName = user.UserName;
            udTemp.Password = user.Password;
            udTemp.EscolaridadNombre = user.EscolaridadNombre;
            udTemp.GrupoEtarioNombre = user.GrupoEtarioNombre;
            List<string> roles = new List<string>();
            foreach (var usuarioRol in _dbContext.UsuarioRol)
                {
                if (usuarioRol.UsuarioCi == user.Ci)
                    roles.Add(usuarioRol.RolNombre);
                }
            udTemp.Roles = roles;
            AgeByCi age = new AgeByCi();
            DateTime actualDate = DateTime.Now;
            udTemp.Edad = age.Get(user.Ci, actualDate);
            udTemp.SexoNombre = user.SexoNombre;

            return udTemp;
            }
        [NonAction]
        public override async Task Post(Usuario1 user)
            {
            /* return new Usuario1();*/
            }

        [HttpPost]
        //[Authorize(Roles = "ADMINISTRADOR,EXAMINADOR")]
        public async Task<IActionResult> Register([FromBody] UserData usuario)
            {
            //creando usuario
            // string newUserNick = usuario.UserName;
            // var usuarioConMismoNick = _dbContext.Usuario.Where(u => u.UserName == newUserNick).SingleOrDefault();
            var usuarioConMismoNick = _dbContext.Usuario.Where(u => u.UserName == usuario.UserName).SingleOrDefault();
            var usuarioConMismoCi = _dbContext.Usuario.Where(u => u.Ci == usuario.Ci).SingleOrDefault();

            if (usuarioConMismoNick != null)
                { return BadRequest("Ya existe un usuario con ese nombre"); }
            if (usuarioConMismoCi != null)
                { return BadRequest("Ya existe un usuario con ese ci"); }
            else
                {
                string grupoEtarioNombre = "Fuera de rango para el estudio";

                // List<string> RolNombres = usuario.Roles;

                string ciAsDate = usuario.Ci.Substring(0, 6);
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
                DateTime actualDate = DateTime.Now;

                // DateTimeOffset userBornDateMs = new DateTimeOffset(userBornDate);
                // DateTimeOffset actualDateMs = new DateTimeOffset(dateToCompare);


                int now = int.Parse(actualDate.ToString("yyyyMMdd"));
                Console.WriteLine(now);
                int dob = int.Parse(userBornDate.ToString("yyyyMMdd"));
                Console.WriteLine(dob);
                int age = (now - dob) / 10000;

                // Console.WriteLine(age);
                /////////////////////

                switch (age)
                    {
                    case < 12:
                        grupoEtarioNombre = "Muy joven";
                        break;
                    /*case int n when (n >= 12 && n <= 18):
                        grupoEtarioNombre = "Joven";
                        break;*/
                    case <= 18:
                        grupoEtarioNombre = "Joven";
                        break;
                    case <= 30:
                        grupoEtarioNombre = "Medio";
                        break;
                    case <= 60:
                        grupoEtarioNombre = "Mayor";
                        break;
                    case > 60:
                        grupoEtarioNombre = "Muy mayor";
                        break;
                    }
                /* if (_dbContext.Usuario.Count() == 3)
                 {
                     RolNombre = "ADMINISTRADOR";
                 }*/
                var userObj = new Usuario1
                    {
                    // UId = Guid.NewGuid(),
                    Nombre = usuario.Nombre,
                    Apellidos = usuario.Apellidos,
                    UserName = usuario.UserName,
                    Password = SecurePasswordHasherHelper.Hash(usuario.Password),
                    // RolNombre = RolNombre,
                    Ci = usuario.Ci,
                    SexoNombre = usuario.SexoNombre,
                    // Edad = usuario.Edad,
                    GrupoEtarioNombre = grupoEtarioNombre,
                    EscolaridadNombre = usuario.EscolaridadNombre,
                    };


                await _dbContext.Usuario.AddAsync(userObj);

                foreach (var rol in usuario.Roles)
                    {
                    UsuarioRol6 urTemp = new UsuarioRol6();
                    urTemp.UsuarioCi = userObj.Ci;
                    urTemp.RolNombre = rol;
                    await _dbContext.UsuarioRol.AddAsync(urTemp);

                    }

                await _dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);
                }
            }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
            {
            Usuario1 usuario = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.UserName == userLogin.UserName);
            List<UsuarioRol6> uRolList = await _dbContext.UsuarioRol.Where(ur => ur.UsuarioCi == usuario.Ci).ToListAsync();
            List<Rol7> rolList = new List<Rol7>();
            foreach (var uRol in uRolList)
                {
                Rol7 rolTemp = await _dbContext.Rol.FirstOrDefaultAsync(u => u.Nombre == uRol.RolNombre);
                if (rolTemp != null)
                    rolList.Add(rolTemp);
                }


            if (usuario == null)
                {
                return NotFound();
                }
            if (!SecurePasswordHasherHelper.Verify(userLogin.Password, usuario.Password))
                {
                return Unauthorized();
                }

            // Rol7 rol =await _dbContext.Rol.FirstOrDefaultAsync(r => r.Nombre == rolNombre.Nombre);
            List<Claim> claims = new List<Claim>();
            Claim cl1 = new Claim(JwtRegisteredClaimNames.UniqueName, userLogin.UserName);
            claims.Add(cl1);
            Claim cl2 = new Claim(ClaimTypes.NameIdentifier, userLogin.UserName);
            claims.Add(cl2);
            foreach (var rol in rolList)
                {
                Claim claimTemp = new Claim(ClaimTypes.Role, rol.Nombre);
                }
            /*var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userLogin.UserName),
                new Claim(ClaimTypes.NameIdentifier, userLogin.UserName),
                new Claim(ClaimTypes.Role, rol.Nombre)
            };*/
            var token = _auth.GenerateAccessToken(claims);
            List<string> roles = new List<string>();
            foreach (var rol in rolList)
                {
                roles.Add(rol.Nombre);
                }
            return new ObjectResult(new
                {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                usuario_id = usuario.Ci,
                roles_name = roles,
                status = 200
                });
            }

        [HttpGet]
        [Route("usuariosConNombre")]
        public async Task<List<Usuario1>> GetAll(string userName)
            {
            return await _dbContext.Set<Usuario1>().Where(uc => uc.UserName.Contains(userName)).ToListAsync();
            }

        [HttpDelete]
        public string DeleteSeveral(List<Guid> ids)
            {
            foreach (var id in ids)
                {
                try
                    {
                    Usuario1 temp = _dbContext.Find<Usuario1>(id);
                    _dbContext.Set<Usuario1>().Remove(temp);
                    }
                catch (Exception)
                    {
                    throw;
                    }


                }
            _dbContext.SaveChanges();
            var databaseName = _dbContext.Database.GetDbConnection().Database;
            return databaseName;
            }

        /*[HttpGet]
        [Route("pruebasActivas")]
        public async Task<List<Examen9>> GetAll(Guid userId)
            {
            List<UsuarioExamen10> ueList =await _dbContext.Set<UsuarioExamen10>().Where(u =>  u.UsuarioCi == userId).ToListAsync();

            List<Examen9> examenes = new List<Examen9>();
            List<Examen9> examenesActivos = new List<Examen9>();

            foreach (var ue in ueList)
                {
                Examen9 exTemp =await _dbContext.FindAsync<Examen9>(ue.ExamenId);
                if(exTemp != null)
                    { examenes.Add(exTemp); }
                }

            foreach (var examen in examenes)
                {
                if(examen.Activo)
                    { examenesActivos.Add(examen); }
                }
            return examenesActivos;
            }*/

        }
    

    }
