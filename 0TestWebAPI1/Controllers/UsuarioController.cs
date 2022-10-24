using _0TestWebAPI1.ClassesForTheApi;
using _0TestWebAPI1.Data;
using _0TestWebAPI1.Models;
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
    public class UsuarioController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        private PruebasDbContext _dbContext;


        public UsuarioController(PruebasDbContext dbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _auth = new AuthService(_configuration);
            _dbContext = dbContext;
        }

       /* [HttpGet]
        [Route("books")]
        public async Task<List<Book>> GetAllBooks()
        {
            var listado = new List<Book>();
            listado = await _dbContext.Books.Include(x => x.BookCategories).ThenInclude(x => x.Category).ToListAsync();
            return listado;
        }*/

       /* public List<PruebaDeCaritas> PcConFilas(ICollection<PruebaDeCaritas> pruebasCaritas)
        {
            List<PruebaDeCaritas> pC = new List<PruebaDeCaritas>();
            foreach (var pc in pruebasCaritas)
            {
                List<Fila> filas = new List<Fila>();
                foreach (var fila in _dbContext.Fila)
                {
                    if (fila.PruebaBaseId == pc.Id)
                    {
                        filas.Add(fila);
                    }
                }
                pc.Filas = filas;
                pC.Add(pc);
            }
            return pC;
        }*/
        [HttpGet]
        [Route("usuarios")]//asdasd
        /*[Authorize(Roles = "ADMINISTRADOR,EXAMINADOR")]*/
        public async Task<IEnumerable<UserData>> GetAll()
        {
            

            var result =
                await
                (from user in _dbContext.Usuario
                     //.Select
                     //(from userCenter in _dbContext.UsuarioCentro
                     // .Where(uc=>uc.UsuarioId==))
                     //.Include(c=>c)
                 select new UserData
                 {
                     Id = user.Id,
                     Ci = user.Ci,
                     Nombre = user.Nombre,
                     Apellidos = user.Apellidos,
                     NickName = user.NickName,
                     Edad = user.Edad,
                     RolNombre = user.RolNombre,
                     //Password = user.Password,
                     SexoNombre = user.SexoNombre,
                     /*Centros = (from uc in _dbContext.UsuarioCentro
                                                  where uc.UsuarioId == user.Id
                                                  join c in _dbContext.Centro
                                                  on uc.CentroId equals c.Id
                                                  select c.Nombre).ToList(),*/
                     GrupoEtarioNombre = user.GrupoEtarioNombre,
                     EscolaridadNombre = user.EscolaridadNombre,
                     /*PruebaDeCaritas = (from pc in user.PruebaDeCaritas
                                        join f in _dbContext.Fila
                                        on pc.Id equals f.PruebaBaseId
                                        select new PruebaDeCaritas { }).ToList()*/
                     /*(from pc in user.PruebaDeCaritas
                      select new PruebaCaritasFormulas {
                          Id = pc.Id,
                          UsuarioId = pc.UsuarioId,
                          Fecha = pc.Fecha,
                          Filas = _dbContext.Fila.Where(f=>f.PruebaBaseId==pc.Id).ToList(),
                          IntentosTotales = pc.IntentosTotales,
                          AnotacionesTotales = pc.AnotacionesTotales,
                          ErroresTotales = pc.ErroresTotales,
                          OmisionesTotales = pc.OmisionesTotales,
                          IGAP = pc.IGAP,
                          ICI = pc.ICI,
                          PorCientoDeAciertos = pc.PorCientoDeAciertos,
                          EficaciaAtencional = pc.EficaciaAtencional,
                          RendimientoAtencional = pc.RendimientoAtencional,
                          CalidadDeLaAtencion = pc.CalidadDeLaAtencion,
                          DatosAtencion= pc.DatosAtencion,
                          Tipo = pc.Tipo
                      }).ToList()*/

                 })
                 .ToListAsync();

            return result;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMINISTRADOR,ADMINISTRADORTESTER")]
        public async Task<UserData> Get(int id)
        {
            Usuario1 user = _dbContext.Usuario.Find(id);

            UserData userData = new UserData();

            userData.Id = user.Id;
            userData.Ci = user.Ci;
            userData.Nombre = user.Nombre;
            userData.Apellidos = user.Apellidos;
            userData.NickName = user.NickName;
            userData.Edad = user.Edad;
            userData.RolNombre = user.RolNombre;
            //Password = user.Password;
            userData.SexoNombre = user.SexoNombre;
            /*userData.Centros = (List<string>)await (from uc in _dbContext.UsuarioCentro
                                                        where uc.UsuarioId == user.Id
                                                        join c in _dbContext.Centro
                                                        on uc.CentroId equals c.Id
                                                        select c.Nombre).ToListAsync();*/
            userData.GrupoEtarioNombre = user.GrupoEtarioNombre;
            userData.EscolaridadNombre = user.EscolaridadNombre;
            /*userData.PruebaDeCaritas = (List<PruebaCaritasFormulas>)user.PruebaDeCaritas;*/

            return userData;
        }

        [HttpPost]
        //[Authorize(Roles = "ADMINISTRADOR,ADMINISTRADORTESTER")]
        public async Task<IActionResult> Register([FromBody] UserData usuario)
        {
            //creando usuario
            string newUserNick = usuario.Nombre +"."+ usuario.Apellidos;
            var usuarioConMismoNick = _dbContext.Usuario.Where(u => u.NickName == newUserNick).SingleOrDefault();
            if (usuarioConMismoNick != null)
            { return BadRequest("Ya existe un usuario con ese nombre"); }
            else
            {
                string grupoEtarioNombre = "Fuera de rango para el estudio";

                string RolNombre = usuario.RolNombre;
                switch (usuario.Edad)
                {
                    case int n when (n >= 12 && n <= 18):
                        grupoEtarioNombre = "Joven";
                        break;
                    case <= 30:
                        grupoEtarioNombre = "Medio";
                        break;
                    case <= 60:
                        grupoEtarioNombre = "Mayor";
                        break;
                }
                if (_dbContext.Usuario.Count() == 3)
                {
                    RolNombre = "ADMINISTRADOR";
                }
                /*int EscolaridadNombre = 1;
                if (usuario.EscolaridadNombre >= 1 && usuario.EscolaridadNombre <= 3)
                {
                    EscolaridadNombre = usuario.EscolaridadNombre;
                }*/
                if (usuario.CentrosIds!=null && usuario.CentrosIds[0] == 0)
                {
                    usuario.CentrosIds = new List<int>();
                }
                var userObj = new Usuario1
                {
                    Nombre = usuario.Nombre,
                    Apellidos = usuario.Apellidos,
                    NickName = newUserNick,
                    Password = SecurePasswordHasherHelper.Hash(usuario.Password)/*usuario.Password*/,
                    RolNombre = RolNombre,
                    Ci = usuario.Ci,
                    SexoNombre = usuario.SexoNombre,
                    Edad = usuario.Edad,
                    GrupoEtarioNombre = grupoEtarioNombre,
                    EscolaridadNombre = usuario.EscolaridadNombre,
                };
                await _dbContext.Usuario.AddAsync(userObj);
               await _dbContext.SaveChangesAsync();

                //asignando los centros a los q pertenece este usuario

              /*  foreach (var centroId in usuario.CentrosIds)
                {
                    UsuarioCentro userCenterTemp = new UsuarioCentro();
                    userCenterTemp.UsuarioId = userObj.Id;
                    userCenterTemp.CentroId = centroId;
                    await _dbContext.UsuarioCentro.AddAsync(userCenterTemp);
                };*/

                await _dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            Usuario1 usuario = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.NickName == userLogin.NickName);
            if (usuario == null)
            {
                return NotFound();
            }
            if (!SecurePasswordHasherHelper.Verify(userLogin.Password, usuario.Password))
            {
                return Unauthorized();
            }

            Rol7 rol =await _dbContext.Rol.FirstOrDefaultAsync(r => r.Nombre == usuario.RolNombre);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userLogin.NickName),
                new Claim(ClaimTypes.NameIdentifier, userLogin.NickName),
                new Claim(ClaimTypes.Role, rol.Nombre)
            };
            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                usuario_id= usuario.Id,
                rol_name = rol.Nombre,
                status=200
            });
        }

        public static int TokenValidTo = 1;

        /*[HttpPost]
        public async Task<IActionResult> Logout([FromBody] UserLogin userLogin)
        {
            Usuario1 usuario = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.NickName == userLogin.NickName);
            if (usuario == null)
            {
                return NotFound();
            }
            if (!SecurePasswordHasherHelper.Verify(userLogin.Password, usuario.Password))
            {
                return Unauthorized();
            }

            Rol7 rol = await _dbContext.Rol.FirstOrDefaultAsync(r => r.Nombre == usuario.RolNombre);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userLogin.NickName),
                new Claim(ClaimTypes.NameIdentifier, userLogin.NickName),
                new Claim(ClaimTypes.Role, rol.Nombre)
            };
            var token = _auth.GenerateAccessToken(claims);
            // token.ValidTo = 1;
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                usuario_id = usuario.Id,
                rol_name = rol.Nombre,
                status = 200
            });
        }*/


        [HttpPut("{id}")]
        /*[Authorize(Roles = "ADMINISTRADOR,ADMINISTRADORTESTER")]*/
        public async Task PutAsync(int id, [FromBody] UserData userData)
        {
            Usuario1 user = await _dbContext.Usuario.FindAsync(id);

            if (user != null)
            {
                /*foreach (var usuarioCentro in _dbContext.UsuarioCentro)
                {
                    if (usuarioCentro.UsuarioId == id)
                    {
                        _dbContext.UsuarioCentro.Remove(usuarioCentro);
                    }
                }

                foreach (var centroId in userData.CentrosIds)
                {
                    UsuarioCentro userCentroTemp = new UsuarioCentro();
                    userCentroTemp.UsuarioId = id;
                    userCentroTemp.CentroId = centroId;
                    await _dbContext.UsuarioCentro.AddAsync(userCentroTemp);
                }*/

                user.Ci = userData.Ci;
                user.Nombre = userData.Nombre;
                user.Apellidos = userData.Apellidos;
                user.NickName = userData.NickName;
                user.Edad = userData.Edad;
                user.RolNombre = userData.RolNombre;
                user.Password = userData.Password;
                user.SexoNombre = userData.SexoNombre;
                user.GrupoEtarioNombre = userData.GrupoEtarioNombre;
                user.EscolaridadNombre = userData.EscolaridadNombre;
                //user.PruebaDeCaritas = userData.PruebaDeCaritas;


                await _dbContext.SaveChangesAsync();
            }

        }


        // DELETE api/<SujetoController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMINISTRADOR,ADMINISTRADORTESTER")]
        public async Task DeleteAsync(int id)
        {
            Usuario1 temp = await _dbContext.Usuario.FindAsync(id);
            _dbContext.Usuario.Remove(temp);
            await _dbContext.SaveChangesAsync();
        }


    }
}
