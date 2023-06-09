using Microsoft.AspNetCore.Mvc;
using SafeLearn.Usuario;

namespace SafeLearn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> logger;
        private readonly IUsuarioService usuarioService;
        public UsuariosController(ILogger<UsuariosController> logger, IUsuarioService usuariosService)
        {
            this.logger = logger;
            this.usuarioService = usuariosService;
        }

        [HttpPost()]
        public async Task<IActionResult> CriarUsuario([FromBody] UsuarioEntity usuario)
        {
            if (usuario == null) return StatusCode(500);
            var newUser = await this.usuarioService.CreateUsuario(usuario);
            await this.usuarioService.UsuarioSuspeito(usuario.Nome);
            return Ok(newUser);
        }
    }
}
