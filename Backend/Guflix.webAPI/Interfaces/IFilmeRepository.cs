using Guflix.webAPI.Domains;

namespace Guflix.webAPI.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório FilmeRepository
    /// </summary>
    interface IFilmeRepository
    {
        /// <summary>
        /// Lista todos os Filmes
        /// </summary>
        List<Filme> ListarTodos();
        /// <summary>
        /// Busca um determinado Filme pelo seu Id
        /// </summary>
        /// <param name="IdFilme"></param>
        Filme BuscarPorId(int IdFilme);
        /// <summary>
        /// Cadastra um novo Filme 
        /// </summary>
        /// <param name="novoFilme"></param>
        void Cadastrar(Filme novoFilme);
        /// <summary>
        /// Atualiza os dados de um determinado Filme cujo Id é existente
        /// </summary>
        /// <param name="IdFilme"></param>
        /// <param name="filmeAtualizado"></param>
        void Atualizar(int IdFilme, Filme filmeAtualizado);
        /// <summary>
        /// Deleta um Filme cujo Id é existente
        /// </summary>
        /// <param name="IdFilme"></param>
        void Deletar(int IdFilme);

    }
}