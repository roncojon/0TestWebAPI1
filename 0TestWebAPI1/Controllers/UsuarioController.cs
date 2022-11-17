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
    public class UsuarioController : ControllerSuper<Usuario1, Guid>
    {
        private readonly IConfiguration _configuration; /* readonly */
        private readonly AuthService _auth; 
        // private readonly PruebasDbContext _dbContext;   /* readonly */

        public UsuarioController(PruebasDbContext context, IConfiguration configuration): base(context)
        {
            _configuration = configuration;
            _auth = new AuthService(_configuration);
           /* _dbContext = dbContext;*/
        }
        [NonAction]
        public override async Task Post(Usuario1 user)
        {
           /* return new Usuario1();*/
        }

        [HttpPost]
        //[Authorize(Roles = "ADMINISTRADOR,EXAMINADOR")]
        public async Task<IActionResult> Register([FromBody] Usuario1 usuario)
        {
            //creando usuario
            // string newUserNick = usuario.NickName;
            // var usuarioConMismoNick = _dbContext.Usuario.Where(u => u.NickName == newUserNick).SingleOrDefault();
            var usuarioConMismoNick = _dbContext.Usuario.Where(u => u.NickName == usuario.NickName).SingleOrDefault();
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
                var userObj = new Usuario1
                {
                    UId = Guid.NewGuid(),
                    Nombre = usuario.Nombre,
                    Apellidos = usuario.Apellidos,
                    NickName = usuario.NickName,
                    Password = SecurePasswordHasherHelper.Hash(usuario.Password),
                    RolNombre = RolNombre,
                    Ci = usuario.Ci,
                    SexoNombre = usuario.SexoNombre,
                    Edad = usuario.Edad,
                    GrupoEtarioNombre = grupoEtarioNombre,
                    EscolaridadNombre = usuario.EscolaridadNombre,
                };
                await _dbContext.Usuario.AddAsync(userObj);
               await _dbContext.SaveChangesAsync();

                await _dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            Usuario1 usuario = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.NickName == userLogin.NickName  );
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
                usuario_id= usuario.UId,
                rol_name = rol.Nombre,
                status=200
            });
        }

        [HttpGet]
        [Route("usuariosConNombre")]
        public async Task<List<Usuario1>> GetAll(string userName)
            {
            return await _dbContext.Set<Usuario1>().Where(uc => uc.NickName.Contains(userName)).ToListAsync();
            }

        [HttpDelete]
        public  void DeleteSeveral(List<Guid> ids)
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

            }

        /*[HttpGet]
        [Route("pruebasActivas")]
        public async Task<List<Examen9>> GetAll(Guid userId)
            {
            List<UsuarioExamen10> ueList =await _dbContext.Set<UsuarioExamen10>().Where(u =>  u.UsuarioId == userId).ToListAsync();

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
