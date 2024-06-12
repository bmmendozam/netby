using back.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using back.Repositorio;

namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UsuarioDAL _usuarioDAL;

        public LoginController(IConfiguration configuration, UsuarioDAL usuarioDAL)
        {
            _configuration = configuration;
            _usuarioDAL = usuarioDAL;
        }

        [HttpPost("Login")]
        public ActionResult<Usuarios> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid client request");
            }

            var usuario = _usuarioDAL.GetUsuario(loginModel.Usuario, loginModel.Clave);
            if (usuario == null)
            {
                return Unauthorized();
            }

            return Ok(usuario);
        }
    }



    public class LoginModel
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }
    }
}
