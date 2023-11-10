using System.ComponentModel.DataAnnotations;

namespace Guflix.webAPI.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mail inválido")]
        public string email { get; set; }
        [Required(ErrorMessage = "Senha inválida")]
        public string password { get; set; }
    }
}
