using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API_EF.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }

        [HttpDelete, Route("{id}")]
        //[ApiExplorerSettings(IgnoreApi = true)] //Oculta el endpoint
        public IActionResult DeleteUsuario(int id)
        {
            try
            {
                _usuarioRepository.Delete(id);
                return Ok("Se borro con exito");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("")]
        public IActionResult GetUsuarios()
        {
            return Ok(_usuarioRepository.GetUsuarios());
        }

        [HttpGet, Route("{id}")]
        public IActionResult GetUsuarioById(int id)
        {
            var empleado = _usuarioRepository.GetUsuarioById(id);
            if(empleado == null)
                return NotFound($"No se encontro al usuario con id: {id}");
            return Ok(empleado);
        }

        [HttpPost, Route("")]
        public IActionResult InsertUsuario([FromBody]UsuarioRequest usuario)
        {
            var user = new Usuario
            {
                Usuario1 = usuario.usuario,
                Nombre = usuario.nombre,
                Mail = usuario.mail,
                Rol = null,
                CreatedAt = DateTime.Now,
                Password = string.Empty
            };
            _usuarioRepository.AddUsuario(user);
            return Ok("Se creo con exito"); 
        }

        [HttpPut, Route("{id}")]
        public IActionResult UpdatePassword(int id, [Required][DataType(DataType.Password)] string pass)
        {
            try
            {
                _usuarioRepository.SetPassword(id, pass);
                var user = _usuarioRepository.GetUsuarioById(id);
                return Ok($"El usuario {user.Usuario1} se modifico con exito");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}