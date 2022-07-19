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

        [HttpGet]
        [Route("books")]
        public async Task<List<Book>> GetAllBooks()
        {
            var listado = new List<Book>();
            listado = await _dbContext.Books.Include(x => x.BookCategories).ThenInclude(x => x.Category).ToListAsync();
            return listado;
        }

        [HttpGet]
        [Route("usuarios")]//asdasd
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
                     RolId = user.RolId,
                     //Password = user.Password,
                     Sexo = user.Sexo,
                     Centros = /*(List<string>)*/(from uc in _dbContext.UsuarioCentro
                                                  where uc.UsuarioId == user.Id
                                                  join c in _dbContext.Centro
                                                  on uc.CentroId equals c.Id
                                                  select c.Nombre).ToList(),
                     GrupoEtarioId = user.GrupoEtarioId,
                     EscolaridadId = user.EscolaridadId,
                     PruebaDeCaritas = user.PruebaDeCaritas.ToList()
                 })
                 .ToListAsync();

            return result;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<UserData> Get(int id)
        {
            Usuario user = _dbContext.Usuario.Find(id);

            UserData userData = new UserData();

            userData.Id = user.Id;
            userData.Ci = user.Ci;
            userData.Nombre = user.Nombre;
            userData.Apellidos = user.Apellidos;
            userData.NickName = user.NickName;
            userData.Edad = user.Edad;
            userData.RolId = user.RolId;
            //Password = user.Password;
            userData.Sexo = user.Sexo;
            userData.Centros = /*(List<string>)*/await (from uc in _dbContext.UsuarioCentro
                                                        where uc.UsuarioId == user.Id
                                                        join c in _dbContext.Centro
                                                        on uc.CentroId equals c.Id
                                                        select c.Nombre).ToListAsync();
            userData.GrupoEtarioId = user.GrupoEtarioId;
            userData.EscolaridadId = user.EscolaridadId;
            userData.PruebaDeCaritas = (List<PruebaDeCaritas>)user.PruebaDeCaritas;

            return userData;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserData usuario)
        {
            //creando usuario
            string newUserNick = usuario.Nombre +"."+ usuario.Apellidos;
            var usuarioConMismoNick = _dbContext.Usuario.Where(u => u.NickName == newUserNick).SingleOrDefault();
            if (usuarioConMismoNick != null)
            { return BadRequest("Ya existe un usuario con ese nombre"); }
            else
            {
                int grupoEtarioId = 1;
                int rolId = 1;
                switch (usuario.Edad)
                {
                    case <= 39:
                        grupoEtarioId = 1;
                        break;
                    case <= 59:
                        grupoEtarioId = 2;
                        break;
                    case >= 60:
                        grupoEtarioId = 3;
                        break;
                }
                if (_dbContext.Usuario.Count() == 2)
                {
                    rolId = 2;
                }
                int escolaridadId = 1;
                if (usuario.EscolaridadId >= 1 && usuario.EscolaridadId <= 3)
                {
                    escolaridadId = usuario.EscolaridadId;
                }
                if (usuario.CentrosIds[0] == 0)
                {
                    usuario.CentrosIds = new List<int>();
                }
                var userObj = new Usuario
                {
                    Nombre = usuario.Nombre,
                    Apellidos = usuario.Apellidos,
                    NickName = newUserNick,
                    Password = SecurePasswordHasherHelper.Hash(usuario.Password),
                    RolId = rolId,
                    Ci = usuario.Ci,
                    Sexo = usuario.Sexo,
                    Edad = usuario.Edad,
                    GrupoEtarioId = grupoEtarioId,
                    EscolaridadId = escolaridadId,
                };
                _dbContext.Usuario.Add(userObj);
                _dbContext.SaveChanges();

                //asignando los centros a los q pertenece este usuario

                foreach (var centroId in usuario.CentrosIds)
                {
                    UsuarioCentro userCenterTemp = new UsuarioCentro();
                    userCenterTemp.UsuarioId = userObj.Id;
                    userCenterTemp.CentroId = centroId;
                    await _dbContext.UsuarioCentro.AddAsync(userCenterTemp);
                };

                await _dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            Usuario usuario = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.NickName == userLogin.NickName);
            if (usuario == null)
            {
                return NotFound();
            }
            if (!SecurePasswordHasherHelper.Verify(userLogin.Password, usuario.Password))
            {
                return Unauthorized();
            }

            Rol rol =await _dbContext.Rol.FirstOrDefaultAsync(r => r.Id == usuario.RolId);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userLogin.NickName),
                new Claim(ClaimTypes.NameIdentifier, userLogin.NickName),
                new Claim(ClaimTypes.Role, rol.NombreDelRol)
            };
            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                usuario_id= usuario.Id
            });

        }

        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] UserData userData)
        {
            Usuario user = await _dbContext.Usuario.FindAsync(id);

            if (user != null)
            {
                foreach (var usuarioCentro in _dbContext.UsuarioCentro)
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
                }

                user.Ci = userData.Ci;
                user.Nombre = userData.Nombre;
                user.Apellidos = userData.Apellidos;
                user.NickName = userData.NickName;
                user.Edad = userData.Edad;
                user.RolId = userData.RolId;
                user.Password = userData.Password;
                user.Sexo = userData.Sexo;
                user.GrupoEtarioId = userData.GrupoEtarioId;
                user.EscolaridadId = userData.EscolaridadId;
                //user.PruebaDeCaritas = userData.PruebaDeCaritas;


                await _dbContext.SaveChangesAsync();
            }

        }


        // DELETE api/<SujetoController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            Usuario temp = await _dbContext.Usuario.FindAsync(id);
            _dbContext.Usuario.Remove(temp);
            await _dbContext.SaveChangesAsync();
        }


    }
}
