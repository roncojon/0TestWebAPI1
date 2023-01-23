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
    // [Authorize(Roles = "ADMINISTRADOR,EXAMINADOR")]
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
        public override async Task Post(Usuario1 user) { }

        [HttpGet]
        [Authorize(Roles = "EXAMINADOR,ADMINISTRADOR")]
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
        public override async Task<Usuario1> GetById(string id)
        { return new Usuario1(); }

        [HttpGet("{ci}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<UserDataGet> GetPlusByCi(string ci)
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
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Register([FromBody] UserDataPost userData)
        {
            UserRegister usuario = userData.Usuario;

            var usuarioConMismoCi = _dbContext.Usuario.Where(u => u.Ci == usuario.Ci).SingleOrDefault();

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
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Modify(string ci, [FromBody] UserDataPost userData)
        {
            try
            {
                UserRegister usuario = userData.Usuario;
                var usuarioConMismoCi = _dbContext.Usuario.Where(u => u.Ci == ci).SingleOrDefault();

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
                    var userObj = new Usuario1
                    {
                        Nombre = usuario.Nombre,
                        Apellidos = usuario.Apellidos,
                        Password = usuario.Password.Length > 0 ? SecurePasswordHasherHelper.Hash(usuario.Password) : usuarioConMismoCi.Password,
                        Ci = usuario.Ci,
                        SexoUId = usuario.SexoUId,
                        GrupoEtarioUId = geTemp.UId/*usuario.GrupoEtarioUId*/,
                        EscolaridadUId = usuario.EscolaridadUId,
                    };
                    usuarioConMismoCi.Nombre = userObj.Nombre;
                    usuarioConMismoCi.Apellidos = userObj.Apellidos;
                    usuarioConMismoCi.Password = userObj.Password;
                    usuarioConMismoCi.SexoUId = userObj.SexoUId;
                    usuarioConMismoCi.GrupoEtarioUId = userObj.GrupoEtarioUId;
                    usuarioConMismoCi.EscolaridadUId = userObj.EscolaridadUId;


                    // I LEFT HEREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
                    _dbContext.Entry(usuarioConMismoCi).State = EntityState.Modified;

                    List<UsuarioRol6> allUserRol = await _dbContext.UsuarioRol.ToListAsync();
                    foreach (var item in allUserRol)
                    {
                       if (item.UsuarioCi== usuarioConMismoCi.Ci)
                            _dbContext.UsuarioRol.Remove(item);
                    }
                    await _dbContext.SaveChangesAsync();

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
                }
            }
            catch
            {
                return BadRequest("Error al salvar los datos");
            }
        }

        [HttpPost]
        [AllowAnonymous]
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
                claims.Add(claimTemp);
            }
            /*Claim claimTemp = new Claim(ClaimTypes.Role, rolList[0].Nombre);
            claims.Add(claimTemp);*/

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
        [Authorize(Roles = "ADMINISTRADOR")]
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
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<RequiredDataForCreateUser> GetDataRequiredDataForCreateUser()
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
        [Authorize(Roles = "ADMINISTRADOR")]
        public IActionResult DeleteSeveral(List<string> ciList)
        {
            try
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
                        // throw;
                        return BadRequest("Error al eliminar");
                    }
                }
            }
            catch
            {
                return BadRequest("Error al eliminar");
            }
            _dbContext.SaveChanges();
            return new OkResult();
        }
    }
}
