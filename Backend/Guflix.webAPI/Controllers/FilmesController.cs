using Guflix.webAPI.Domains;
using Guflix.webAPI.Interfaces;
using Guflix.webAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Guflix.webAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        /// <summary>
        /// Objeto _filmeRepository que receberá todos os métodos definidos na interface IFilmeRepository
        /// </summary>
        private IFilmeRepository _filmeRepository { get; set; }

        /// <summary>
        /// Instância do objeto "_filmeRepository" para que seja possível fazer uso das implementações 
        ///  realizadas no repositório "FilmeRepository".
        /// </summary>
        public FilmesController()
        {
            _filmeRepository = new FilmeRepository();
        }

        /// <summary>
        /// Lista todos os Filmes
        /// </summary>
        /// <returns>Uma lista de filmes com Status code 200 - Ok</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<Filme> listaFilme = _filmeRepository.ListarTodos();
                return Ok(listaFilme);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Busca o Filme através do seu Id
        /// </summary>
        /// <param name="IdFilme">Id do Filme que será buscado</param>
        /// <returns>Um Filme encontrado com o Status code 200 - Ok</returns>
        [HttpGet("{IdFilme}")]
        public IActionResult BuscarPorId(int IdFilme)
        {
            Filme filmeBuscado = _filmeRepository.BuscarPorId(IdFilme);
            if (filmeBuscado == null)
            {
                return NotFound
                    (new
                    {
                        messagem = "Filme não encontrado",
                        erro = true
                    });
            }
            try
            {
                // Retorna um Filme encontrado
                return Ok(filmeBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Cadastra um Filme
        /// </summary>
        /// <param name="novoFilme">Objeto novoFilme com as informações</param>
        /// <returns>Status code 201 - Created</returns>
        public IActionResult Cadastrar(Filme novoFilme)
        {
            try
            {
                // Faz a chamada para o método Cadastrar enviando as informações de cadastro
                _filmeRepository.Cadastrar(novoFilme);
                // Retorna um status code 201 Created
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Atualiza um Filme existente
        /// </summary>
        /// <param name="IdFilme">Id do Filme que será atualizado</param>
        /// <param name="filmeAtualizado">Objeto filmeAtualizado com as novas informações</param>
        /// <returns>Um Status code 204</returns>
        [HttpPut("{IdFilme}")]
        public IActionResult Atualizar(int IdFilme, Filme filmeAtualizado)
        {
            Filme filmeBuscado = _filmeRepository.BuscarPorId(IdFilme);
            if (filmeBuscado == null)
            {
                return NotFound
                    (new
                    {
                        mensagem = "Filme não encontrado",
                        erro = true
                    });
            }
            try
            {
                _filmeRepository.Atualizar(IdFilme, filmeAtualizado);
                // Retorna um Status Code 204
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Deleta um Filme existente
        /// </summary>
        /// <param name="IdFilme">Id do Filme que será deletado</param>
        /// <returns>Status Code 204  No Content</returns>
        [HttpDelete("{IdFilme}")]
        public IActionResult Deletar(int IdFilme)
        {
            Filme filmeBuscado = _filmeRepository.BuscarPorId(IdFilme);
            if (filmeBuscado != null)
            {
                try
                {
                    _filmeRepository.Deletar(IdFilme);
                    return StatusCode(204);
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }
            }
            return NotFound("Filme não encontrado");
        }

    }
}

