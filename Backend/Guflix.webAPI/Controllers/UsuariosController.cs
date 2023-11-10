using Guflix.webAPI.Domains;
using Guflix.webAPI.Interfaces;
using Guflix.webAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Guflix.webAPI.Controllers
{
    /// <summary>
    /// Controller responsável pelos endpoints (URLs) referentes aos Usuários
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        /// <summary>
        /// Objeto _usuarioRepository que receberá todos os métodos definidos na interface IUsuarioRepository
        /// </summary>
        private IUsuarioRepository _usuarioRepository {  get; set; }

        public UsuariosController() 
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpGet]
        public ActionResult Listar() 
        {
            try
            {
                List<Usuario> listaUsuarios = _usuarioRepository.ListarTodos();
                return Ok(listaUsuarios);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet("{IdUser}")]
        public ActionResult BuscarPorId(int IdUser) 
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorId(IdUser);
            if (usuarioBuscado == null)
            {
                return NotFound
                    (new
                    {
                        mensagem = "Usuário não encontrado",
                        erro = true
                    });
            }
            try
            {
                return Ok(usuarioBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario NovoUsuario)
        {
            try
            {
                _usuarioRepository.Cadastrar(NovoUsuario);
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPut("{IdUser}")]
        public IActionResult Atualizar(int IdUser, Usuario usuarioAtualizado)
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorId(IdUser);
            if (usuarioBuscado == null)
            {
                return NotFound
                    (new
                    {
                        mensagem = "Usuário não encontrado",
                        erro = true
                    });
            }
            try
            {
                _usuarioRepository.Atualizar(IdUser, usuarioAtualizado);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);    
            }
        }

        [HttpDelete("{IdUser}")]  
        public IActionResult Deletar(int IdUser)
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorId(IdUser);
            if (usuarioBuscado != null)
            {
                try
                {
                    _usuarioRepository.Deletar(IdUser);
                    return StatusCode(204);
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }
            }
            return NotFound("Usuário não encontrado");
        }
    }
}
