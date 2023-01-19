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
        [NonAction]
        public override async Task<List<Usuario1>> GetAll() { return new List<Usuario1>(); }
        [NonAction]
        public override async Task Post(Usuario1 user) {/* return new Usuario1();*/}
        [HttpGet]
        // [Authorize]
        [Route("usuariosplus")]
        public async Task<List<UserDataGet>> GetAllPlus()
            {
            List<Usuario1> usuarios = await _dbContext.Set<Usuario1>().ToListAsync();
            List<UserDataGet> usuariosPlus = new List<UserDataGet>();

            foreach (var user in usuarios)
                {
                UserDataGet udTemp = new UserDataGet();
                udTemp.Ci = user.Ci;
                udTemp.Nombre = user.Nombre;
                udTemp.Apellidos = user.Apellidos;
                // udTemp.UserName = user.UserName;
                udTemp.Password = user.Password;

                // Hayando la escolaridad
                Escolaridad es = await _dbContext.Escolaridad.FirstOrDefaultAsync(e => e.UId == user.EscolaridadUId);
                udTemp.EscolaridadNombre = es.Nombre;

                // Hayando el grupo etario
                GrupoEtario ge = await _dbContext.GrupoEtario.FirstOrDefaultAsync(g => g.UId == user.GrupoEtarioUId);
                udTemp.GrupoEtarioNombre = ge.Nombre;

                // Hayando los roles
                List<Rol7> roles = new List<Rol7>();

                List<UsuarioRol6> urList = _dbContext.UsuarioRol.ToList();
                List<Rol7> rolList = _dbContext.Rol.ToList();

                foreach (var usuarioRol in urList)
                    {
                    if (usuarioRol.UsuarioCi == user.Ci)
                        {
                        foreach (var rol in rolList)
                            {
                            if (rol.UId == usuarioRol.RolUId)
                                roles.Add(rol);
                            }
                        }
                    }
                udTemp.Roles = roles;
                //

                // Hayando la edad
                AgeByCi age = new AgeByCi();
                DateTime actualDate = DateTime.Now;
                udTemp.Edad = age.Get(user.Ci, actualDate);
                //

                // Hayando el sexo
                Sexo2 sx = await _dbContext.Sexo.FirstOrDefaultAsync(s => s.UId == user.SexoUId);
                udTemp.SexoNombre = sx.Nombre;
                //

                usuariosPlus.Add(udTemp);
                }
            return usuariosPlus;
            }
        [NonAction]
        public override async Task<Usuario1> GetById(string id)  // public async Task<ActionResult<T>> GetById(Z id)
            { return new Usuario1(); }
        // [Route("getbyci")]
        [HttpGet("{ci}")]
        public async Task<UserDataGet> GetPlusByCi(string ci)  // public async Task<ActionResult<T>> GetById(Z id)
            {

            Usuario1 user = await _dbContext.FindAsync<Usuario1>(ci);

            UserDataGet udTemp = new UserDataGet();
            udTemp.Ci = user.Ci;
            udTemp.Nombre = user.Nombre;
            udTemp.Apellidos = user.Apellidos;
            // udTemp.UserName = user.UserName;
            udTemp.Password = user.Password;

            // Hayando la escolaridad
            Escolaridad es = await _dbContext.Escolaridad.FirstOrDefaultAsync(e => e.UId == user.EscolaridadUId);
            udTemp.EscolaridadNombre = es.Nombre;

            // Hayando el grupo etario
            GrupoEtario ge = await _dbContext.GrupoEtario.FirstOrDefaultAsync(g => g.UId == user.GrupoEtarioUId);
            udTemp.GrupoEtarioNombre = ge.Nombre;

            // Hayando los roles
            List<Rol7> roles = new List<Rol7>();

            List<UsuarioRol6> urList = _dbContext.UsuarioRol.ToList();
            List<Rol7> rolList = _dbContext.Rol.ToList();

            foreach (var usuarioRol in urList)
                {
                if (usuarioRol.UsuarioCi == user.Ci)
                    {
                    foreach (var rol in rolList)
                        {
                        if (rol.UId == usuarioRol.RolUId)
                            roles.Add(rol);
                        }
                    }
                }
            udTemp.Roles = roles;
            AgeByCi age = new AgeByCi();
            DateTime actualDate = DateTime.Now;
            udTemp.Edad = age.Get(user.Ci, actualDate);

            // Hayando el sexo
            Sexo2 sx = await _dbContext.Sexo.FirstOrDefaultAsync(s => s.UId == user.SexoUId);
            udTemp.SexoNombre = sx.Nombre;

            return udTemp;
            }


        [HttpPost]
        //[Authorize(Roles = "ADMINISTRADOR,EXAMINADOR")]
        public async Task<IActionResult> Register([FromBody] UserDataPost userData)
            {
            UserRegister usuario = userData.Usuario;
            //creando usuario
            // string newUserNick = usuario.Ci;
            // var usuarioConMismoNick = _dbContext.Usuario.Where(u => u.Ci == newUserNick).SingleOrDefault();

            // var usuarioConMismoNick = _dbContext.Usuario.Where(u => u.Ci == usuario.Ci).SingleOrDefault();
            var usuarioConMismoCi = _dbContext.Usuario.Where(u => u.Ci == usuario.Ci).SingleOrDefault();

            /*if (usuarioConMismoNick != null)
                { return BadRequest("Ya existe un usuario con ese nombre"); }*/
            if (usuarioConMismoCi != null)
                { return BadRequest("Ya existe un usuario con ese ci"); }
            else
                {
                string ciAsDate = usuario.Ci.Substring(0, 6);
                string ciAsDateYear = ciAsDate.Substring(0, 2);

                ///////////////////// EDAD
                int edad = 0;
                AgeByCi age = new AgeByCi();
                DateTime actualDate = DateTime.Now;
                edad = age.Get(usuario.Ci, actualDate);

                /*  string actualYear = DateTime.Now.Year.ToString();
                  if (int.Parse(ciAsDateYear) <= int.Parse(actualYear.Substring(2, 2)))
                      {
                      ciAsDate = "20" + ciAsDate;
                      }
                  else
                      {
                      ciAsDate = "19" + ciAsDate;
                      }
                  ciAsDateYear = ciAsDate.Substring(0, 4);
                  string ciAsDateMonth = ciAsDate.Substring(4, 2);
                  string ciAsDateDay = ciAsDate.Substring(6, 2);

                  ciAsDate = ciAsDateMonth + "/" + ciAsDateDay + "/" + ciAsDateYear;
                  string[] ciAsDateArray = { ciAsDateMonth, "/", ciAsDateDay, "/", ciAsDateYear };
                  ciAsDate = string.Concat(ciAsDateArray);

                  DateTime userBornDate = DateTime.Parse(ciAsDate);
                  DateTime actualDate = DateTime.Now;

                  int now = int.Parse(actualDate.ToString("yyyyMMdd"));
                  Console.WriteLine(now);
                  int dob = int.Parse(userBornDate.ToString("yyyyMMdd"));
                  Console.WriteLine(dob);
                  int age = (now - dob) / 10000;*/
                /////////////////////
                if (edad <= 0)
                    { return BadRequest("Error al salvar los datos del Ci"); }

                // HAYANDO GRUPOETARIO OKOK
                GrupoEtario geTemp = new GrupoEtario();
                List<GrupoEtario> geListAll = await _dbContext.GrupoEtario.ToListAsync();
                foreach (var ge in geListAll)
                    {
                    if (DoesThisAgeFitsInThisGroup(ge, edad))
                        geTemp = ge;
                    }
                bool DoesThisAgeFitsInThisGroup(GrupoEtario ge, int edad)
                    {
                    if (edad >= ge.EdadMinima && edad < ge.EdadMaxima)
                        {
                        return true;
                        }
                    else
                        {
                        return false;
                        }
                    }
                ////////////////////

                /*switch (age)
                    {
                    case < 12:
                        grupoEtarioNombre = "Muy joven";
                        break;
                    case int n when (n >= 12 && n <= 18):
                        grupoEtarioNombre = "Joven";
                        break;
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
                    }*/

                var userObj = new Usuario1
                    {
                    // UId = Guid.NewGuid(),
                    Nombre = usuario.Nombre,
                    Apellidos = usuario.Apellidos,
                    // Ci = usuario.Ci,
                    Password = SecurePasswordHasherHelper.Hash(usuario.Password),
                    // RolNombre = RolNombre,
                    Ci = usuario.Ci,
                    SexoUId = usuario.SexoUId,
                    // Edad = usuario.Edad,
                    GrupoEtarioUId = geTemp.UId/*usuario.GrupoEtarioUId*/,
                    // GrupoEtarioNombre = geTemp.Nombre/*usuario.GrupoEtarioUId*/,
                    EscolaridadUId = usuario.EscolaridadUId,
                    };

                await _dbContext.Usuario.AddAsync(userObj);

                foreach (var rolId in userData.RolesUIds)
                    {
                    UsuarioRol6 urTemp = new UsuarioRol6();
                    urTemp.UsuarioCi = userObj.Ci;
                    urTemp.RolUId = rolId;
                    _dbContext.UsuarioRol.Add(urTemp);

                    }

                await _dbContext.SaveChangesAsync();
                return Ok("Creado");

                // return StatusCode(StatusCodes.Status201Created);
                }
            }

        [HttpPut("{ci}")]
        //[Authorize(Roles = "ADMINISTRADOR,EXAMINADOR")]
        public async Task<IActionResult> Modify(string ci, [FromBody] UserDataPost userData)
            {
            try
                {
                UserRegister usuario = userData.Usuario;
                //creando usuario
                // string newUserNick = usuario.Ci;
                // var usuarioConMismoNick = _dbContext.Usuario.Where(u => u.Ci == newUserNick).SingleOrDefault();

                // var usuarioConMismoNick = _dbContext.Usuario.Where(u => u.Ci == usuario.Ci).SingleOrDefault();
                var usuarioConMismoCi = _dbContext.Usuario.Where(u => u.Ci == ci).SingleOrDefault();

                /*if (usuarioConMismoNick != null)
                    { return BadRequest("Ya existe un usuario con ese nombre"); }*/
                if (usuarioConMismoCi == null)
                    { return BadRequest("No existe un usuario con ese ci"); }
                else
                    {
                    string ciAsDate = usuario.Ci.Substring(0, 6);
                    string ciAsDateYear = ciAsDate.Substring(0, 2);

                    /////////////////////
                    int edad = 0;
                    AgeByCi age = new AgeByCi();
                    DateTime actualDate = DateTime.Now;
                    edad = age.Get(usuario.Ci, actualDate);
                    /* string actualYear = DateTime.Now.Year.ToString();
                     if (int.Parse(ciAsDateYear) <= int.Parse(actualYear.Substring(2, 2)))
                         {
                         ciAsDate = "20" + ciAsDate;
                         }
                     else
                         {
                         ciAsDate = "19" + ciAsDate;
                         }
                     ciAsDateYear = ciAsDate.Substring(0, 4);
                     string ciAsDateMonth = ciAsDate.Substring(4, 2);
                     string ciAsDateDay = ciAsDate.Substring(6, 2);

                     ciAsDate = ciAsDateMonth + "/" + ciAsDateDay + "/" + ciAsDateYear;
                     string[] ciAsDateArray = { ciAsDateMonth, "/", ciAsDateDay, "/", ciAsDateYear };
                     ciAsDate = string.Concat(ciAsDateArray);

                     DateTime userBornDate = DateTime.Parse(ciAsDate);
                     DateTime actualDate = DateTime.Now;

                     int now = int.Parse(actualDate.ToString("yyyyMMdd"));
                     Console.WriteLine(now);
                     int dob = int.Parse(userBornDate.ToString("yyyyMMdd"));
                     Console.WriteLine(dob);
                     int age = (now - dob) / 10000;*/
                    /////////////////////
                    if (edad <= 0)
                        { return BadRequest("Error al salvar los datos del Ci"); }

                    // HAYANDO GRUPOETARIO OKOK
                    GrupoEtario geTemp = new GrupoEtario();
                    List<GrupoEtario> geListAll = await _dbContext.GrupoEtario.ToListAsync();
                    bool DoesThisAgeFitsInThisGroup(GrupoEtario ge, int edad)
                        {
                        if (edad >= ge.EdadMinima && edad < ge.EdadMaxima)
                            {
                            return true;
                            }
                        else
                            {
                            return false;
                            }
                        }
                    foreach (var ge in geListAll)
                        {
                        if (DoesThisAgeFitsInThisGroup(ge, edad))
                            geTemp = ge;
                        }

                    ////////////////////

                    /*switch (age)
                        {
                        case < 12:
                            grupoEtarioNombre = "Muy joven";
                            break;
                        case int n when (n >= 12 && n <= 18):
                            grupoEtarioNombre = "Joven";
                            break;
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
                        }*/

                    var userObj = new Usuario1
                        {
                        // UId = Guid.NewGuid(),
                        Nombre = usuario.Nombre,
                        Apellidos = usuario.Apellidos,
                        // Ci = usuario.Ci,
                        Password = usuario.Password.Length > 0 ? SecurePasswordHasherHelper.Hash(usuario.Password) : usuarioConMismoCi.Password,
                        // RolNombre = RolNombre,
                        Ci = usuario.Ci,
                        SexoUId = usuario.SexoUId,
                        // Edad = usuario.Edad,
                        GrupoEtarioUId = geTemp.UId/*usuario.GrupoEtarioUId*/,
                        // GrupoEtarioNombre = geTemp.Nombre/*usuario.GrupoEtarioUId*/,
                        EscolaridadUId = usuario.EscolaridadUId,
                        };
                    _dbContext.Usuario.Remove(usuarioConMismoCi);
                    await _dbContext.SaveChangesAsync();

                    await _dbContext.Usuario.AddAsync(userObj);


                    foreach (var rolId in userData.RolesUIds)
                        {
                        UsuarioRol6 urTemp = new UsuarioRol6();
                        urTemp.UsuarioCi = userObj.Ci;
                        urTemp.RolUId = rolId;
                        await _dbContext.UsuarioRol.AddAsync(urTemp);

                        }

                    await _dbContext.SaveChangesAsync();
                    return new ObjectResult(new
                        {
                        ok = true
                        });
                    // return StatusCode(StatusCodes.Status201Created);
                    }
                }
            catch
                {
                return BadRequest("Error al salvar los datos");
                }
            }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
            {
            // When an user try to login of course he is not authenticated, so in startup.cs the connectionString
            // gonna be option1 if user is not authenticated. With option1 the api will connect to db with just read permissions.
            // if user is authenticated in startup.cs the connectionString gonna be option2. With option2 if the api will connect to db
            // with read/write permissions.
    
            // ConnectionStringHandler.isUserAuthenticated = false; //

            Usuario1 usuario = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.Ci == userLogin.Ci);
            if (usuario == null)
                {
                return NotFound();
                }

            List<UsuarioRol6> uRolList = await _dbContext.UsuarioRol.Where(ur => ur.UsuarioCi == usuario.Ci).ToListAsync();
            List<Rol7> rolList = new List<Rol7>();
            foreach (var uRol in uRolList)
                {
                Rol7 rolTemp = await _dbContext.Rol.FirstOrDefaultAsync(r => r.UId == uRol.RolUId);
                if (rolTemp != null)
                    rolList.Add(rolTemp);
                }


            
            if (!SecurePasswordHasherHelper.Verify(userLogin.Password, usuario.Password))
                {
                return Unauthorized();
                }

            // Rol7 rol =await _dbContext.Rol.FirstOrDefaultAsync(r => r.Nombre == rolNombre.Nombre);
            List<Claim> claims = new List<Claim>();
            Claim cl1 = new Claim(JwtRegisteredClaimNames.UniqueName, userLogin.Ci);
            claims.Add(cl1);
            Claim cl2 = new Claim(ClaimTypes.NameIdentifier, userLogin.Ci);
            claims.Add(cl2);
            foreach (var rol in rolList)
                {
                Claim claimTemp = new Claim(ClaimTypes.Role, rol.Nombre);
                }
            /*var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userLogin.Ci),
                new Claim(ClaimTypes.NameIdentifier, userLogin.Ci),
                new Claim(ClaimTypes.Role, rol.Nombre)
            };*/
            var token = _auth.GenerateAccessToken(claims);

            List<string> roles = new List<string>();
            foreach (var rol in rolList)
                {
                roles.Add(rol.Nombre);
                }

            // changing this prop will change the connection string at startup.cs to option2 wich means that the api
            // will have wread/write permissions in db
            // ConnectionStringHandler.isUserAuthenticated = true; //

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
        public async Task<List<UserDataGet>> GetAll(string userName)
            {
            List<Usuario1> usuarios = await _dbContext.Set<Usuario1>().Where(uc => uc.Nombre.Contains(userName) || uc.Apellidos.Contains(userName)).ToListAsync();

            List<UserDataGet> usuariosPlus = new List<UserDataGet>();

            foreach (var user in usuarios)
                {
                UserDataGet udTemp = new UserDataGet();
                udTemp.Ci = user.Ci;
                udTemp.Nombre = user.Nombre;
                udTemp.Apellidos = user.Apellidos;
                // udTemp.UserName = user.UserName;
                udTemp.Password = user.Password;

                // Hayando la escolaridad
                Escolaridad es = await _dbContext.Escolaridad.FirstOrDefaultAsync(e => e.UId == user.EscolaridadUId);
                udTemp.EscolaridadNombre = es.Nombre;

                // Hayando el grupo etario
                GrupoEtario ge = await _dbContext.GrupoEtario.FirstOrDefaultAsync(g => g.UId == user.GrupoEtarioUId);
                udTemp.GrupoEtarioNombre = ge.Nombre;

                // Hayando los roles
                List<Rol7> roles = new List<Rol7>();

                List<UsuarioRol6> urList = _dbContext.UsuarioRol.ToList();
                List<Rol7> rolList = _dbContext.Rol.ToList();

                foreach (var usuarioRol in urList)
                    {
                    if (usuarioRol.UsuarioCi == user.Ci)
                        {
                        foreach (var rol in rolList)
                            {
                            if (rol.UId == usuarioRol.RolUId)
                                roles.Add(rol);
                            }
                        }
                    }
                udTemp.Roles = roles;
                //

                // Hayando la edad
                AgeByCi age = new AgeByCi();
                DateTime actualDate = DateTime.Now;
                udTemp.Edad = age.Get(user.Ci, actualDate);
                //

                // Hayando el sexo
                Sexo2 sx = await _dbContext.Sexo.FirstOrDefaultAsync(s => s.UId == user.SexoUId);
                udTemp.SexoNombre = sx.Nombre;
                //

                usuariosPlus.Add(udTemp);
                }
            return usuariosPlus;
            }

        [HttpGet]
        public async Task<RequiredDataForCreateUser> GetDataRequiredDataForCreateUser()  // public async Task<ActionResult<T>> GetById(Z id)
            {
            RequiredDataForCreateUser result = new RequiredDataForCreateUser();
            List<Rol7> Roles = await _dbContext.Rol.ToListAsync();
            List<Escolaridad> EscolaridadList = await _dbContext.Escolaridad.ToListAsync();
            List<Sexo2> SexoList = await _dbContext.Sexo.ToListAsync();
            result.Roles = Roles;
            result.EscolaridadList = EscolaridadList;
            result.SexoList = SexoList;

            return result;
            }

        [HttpDelete]
        public IActionResult /*async Task<IActionResult>*/ DeleteSeveral(List<string> ciList)
            {
            foreach (var ci in ciList)
                {
                try
                    {
                    Usuario1 temp = _dbContext.Find<Usuario1>(ci);
                    _dbContext.Set<Usuario1>().Remove(temp);

                    }
                catch (Exception)
                    {
                    throw;
                    return BadRequest("Error al eliminar");
                    }


                }
            _dbContext.SaveChanges();
            return new OkResult();
            // return BadRequest("Error al eliminar 2");

            // var databaseName = _dbContext.Database.GetDbConnection().Database;
            // return databaseName;
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
