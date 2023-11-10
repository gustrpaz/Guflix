using Guflix.webAPI.Contexts;
using Guflix.webAPI.Domains;
using Guflix.webAPI.Interfaces;
using Guflix.webAPI.Utils;

namespace Guflix.webAPI.Repositories
{
    /// <summary>
    /// Classe Responsável pelo Repository de Usuário
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        GuflixContext ctx = new GuflixContext();
        /// <summary>
        /// Método Atualizar as propriedades de um Usuário
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <param name="usuarioAtualizado"></param>
        /// <returns></returns>
        public void Atualizar(int IdUsuario, Usuario usuarioAtualizado)
        {
            Usuario usuarioBuscado = BuscarPorId(IdUsuario);
            if (usuarioAtualizado.UserName != null)
            {
                // Caso exista, suas propriedades são atualizadas
                usuarioBuscado.UserName = usuarioAtualizado.UserName;
                usuarioBuscado.Email = usuarioAtualizado.Email;
                usuarioBuscado.Passwd = usuarioAtualizado.Passwd;
            }
            // Atualiza o Usuário que foi buscado na Lista
            ctx.Usuarios.Update(usuarioBuscado);
            // Salva as informações que serão gravadas no Banco de Dados
            ctx.SaveChanges();
        }
        /// <summary>
        /// Método responsável por Buscar um Usuário através de um Id
        /// </summary>
        /// <param name="IdUser"></param>
        /// <returns>Retorna Usuário com o Id informado</returns>
        public Usuario BuscarPorId(int IdUser)
        {
            // Salva o Primeiro Usuário encontrado para o ID informado
            return ctx.Usuarios.FirstOrDefault(u => u.IdUser == IdUser)!;
        }
        /// <summary>
        /// Método feito para Cadastrar um usuário novo
        /// </summary>
        /// <param name="novoUsuario"></param>
        public void Cadastrar(Usuario novoUsuario)
        {
            // Adiciona um novo usuário
            ctx.Usuarios.Add(novoUsuario);
            // Salva as informações que serão gravadas no Banco de Dados
            ctx.SaveChanges();
        }
        /// <summary>
        /// Método para Deletar um usuário existente
        /// </summary>
        /// <param name="IdUser"></param>
        public void Deletar(int IdUser)
        {
            Usuario usuarioBuscado = BuscarPorId(IdUser);
            ctx.Usuarios.Remove(usuarioBuscado);
            ctx.SaveChanges();
        }
        /// <summary>
        /// Método que lista todos Usuários
        /// </summary>
        /// <returns>Retorna a lista com todos os usuários</returns>
        public List<Usuario> ListarTodos()
        {
            // Retorna a lista com todas as informações dos Usuários
            return ctx.Usuarios.Select(u => new Usuario
            {
                IdUser = u.IdUser,
                Email = u.Email,
                Passwd = u.Passwd
            }).ToList();
        }
        /// <summary>
        /// Cria um Hash para a senha
        /// </summary>
        /// <param name="_usuario"></param>
        public async void EncryptPassword(Usuario _usuario)
        {
            _usuario.Passwd = Crypt.GenerateHash(_usuario.Passwd);
            ctx.Usuarios.Update(_usuario);
            await ctx.SaveChangesAsync();
        }
        /// <summary>
        /// Faz o Login através do E-mail e da Senha
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Usuario Login(string email, string password)
        {
            var usuario = ctx.Usuarios.FirstOrDefault(u => u.Email == email);
            if (usuario.Passwd == null)
            {
                return usuario;
            }
            if (usuario != null)
            {
                if (Crypt.Validate(usuario.Passwd) == true)
                {
                    bool IsEncrypted = Crypt.Compare(password, usuario.Passwd);
                    if (IsEncrypted)
                    {
                        return usuario;
                    }
                }
                else
                {
                    EncryptPassword(usuario);
                    bool IsEncrypted = Crypt.Compare(password, usuario.Passwd);
                    if (IsEncrypted)
                        return usuario;
                }
            }
            return null;
        }
        /// <summary>
        /// Atualiza os dados de um determinado Usuário cujo Id é existente
        /// </summary>
        /// <param name="IdUser"></param>
        /// <param name="usuarioAtualizado"></param>
        /// <returns></returns>
        void IUsuarioRepository.Atualizar(int IdUsuario, Usuario usuarioAtualizado)
        {
            Usuario usuarioBuscado = BuscarPorId(IdUsuario);
            if (usuarioAtualizado.UserName != null)
            {
                usuarioBuscado.UserName = usuarioAtualizado.UserName;
                usuarioBuscado.Email = usuarioAtualizado.Email;
                usuarioBuscado.Passwd = usuarioAtualizado.Passwd;

            }
            ctx.Usuarios.Update(usuarioBuscado);
            ctx.SaveChanges();
        }
        /// <summary>
        /// Método feito para Cadastrar um Usuário novo
        /// </summary>
        /// <param name="novoFilme"></param>
        void IUsuarioRepository.Cadastrar(Usuario novoUsuario)
        {
            ctx.Usuarios.Add(novoUsuario);
            ctx.SaveChanges();
        }
    }
}
