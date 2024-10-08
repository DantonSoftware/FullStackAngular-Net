using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{

    public class usuarioController : BaseApiController
    {
        //private readonly ApplicationDbContext _db;
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly ITokenServicio _tokenServicio;

        public usuarioController(UserManager<UsuarioAplicacion> userManager, ITokenServicio tokenServicio)
        {
            //_db = db;
            _userManager = userManager;
            _tokenServicio = tokenServicio;
        }

        //End Point
        //[Authorize]
        //[HttpGet] //Api/usuario
        //public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        //{
        //    var usuarios = await _db.Usuarios.ToListAsync();
        //    return Ok(usuarios);
        //}

        //[Authorize]
        //[HttpGet("{id}")] //api/usuario/1
        //public async Task<ActionResult<Usuario>> GetUsuario(int id)
        //{
        //    var usuario = await _db.Usuarios.FindAsync(id);
        //    return Ok(usuario);
        //}

        [HttpPost("registro")] //Post: api/usuario/registro
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
        {
            if(await UsuarioExiste(registroDto.Username)) return BadRequest("UserName ya esta Registrado");
            
            var usuario = new UsuarioAplicacion
            {
                UserName = registroDto.Username.ToLower(),
            };

            var resultado = await _userManager.CreateAsync(usuario, registroDto.Password);
            if (!resultado.Succeeded) return BadRequest(resultado.Errors);

            return new UsuarioDto
            {
                Username = usuario.UserName,
                Token = _tokenServicio.CrearToken(usuario)
            };
        }

        [HttpPost("login")] // POST: api/usuario/login
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if(usuario == null) return Unauthorized("Usuario no valido");
            
            var resultado = await _userManager.CheckPasswordAsync(usuario, loginDto.Password);

            if (!resultado) return Unauthorized("Password no valido");

            return new UsuarioDto
            {
                Username = usuario.UserName,
                Token = _tokenServicio.CrearToken(usuario)
            };
        }

        private async Task<bool> UsuarioExiste(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }


    }

  
}
