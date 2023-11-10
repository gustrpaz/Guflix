using Guflix.webAPI.Domains;
namespace Guflix.webAPI.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório UsuarioRepository
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Lista todos os Usuarios
        /// </summary>
        /// <returns></returns>
        List<Usuario> ListarTodos();
        /// <summary>
        /// Busca um determinado Usuário pelo seu ID
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        Usuario BuscarPorId(int IdUser);
        /// <summary>
        /// Cadastra um novo Usuário
        /// </summary>
        /// <param name="novoUsuario"></param>
        /// <returns></returns>
        void Cadastrar(Usuario novoUsuario);
        /// <summary>
        /// Atualiza os dados de um determinado Usuário cujo Id é existente
        /// </summary>
        /// <param name="IdUser"></param>
        /// <param name="usuarioAtualizado"></param>
        /// <returns></returns>
        void Atualizar(int IdUser, Usuario usuarioAtualizado);
        /// <summary>
        /// Deleta um Usuário cujo Id é existente
        /// </summary>
        /// <param name="IdUser"></param>
        void Deletar(int IdUser);
        /// <summary>
        /// Cria um Hash para a senha
        /// </summary>
        /// <param name="_usuario"></param>
        void EncryptPassword(Usuario _usuario);
        /// <summary>
        /// Faz o Login através do E-mail e da Senha
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Usuario Login(string email, string password);
    }
}
