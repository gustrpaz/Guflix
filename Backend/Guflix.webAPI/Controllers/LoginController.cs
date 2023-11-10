using Guflix.webAPI.Domains;
using Guflix.webAPI.Interfaces;
using Guflix.webAPI.Repositories;
using Guflix.webAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Guflix.webAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public LoginController ()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel UsuarioLogin)
        {
            try
            {
                Usuario queryUsuario = _usuarioRepository.Login(UsuarioLogin.email,UsuarioLogin.password);
                if (queryUsuario == null)
                {
                    return Unauthorized(new { msg = "Email ou senha inválidos" });
                }
                var tokenClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, queryUsuario.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, queryUsuario.IdUser.ToString()),

                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("gustavoflix-token-para-autenticacao"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var myToken = new JwtSecurityToken(
                        issuer: "GUFLIX_API",
                        audience: "GUFLIX_API",
                        claims: tokenClaims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(myToken)
                });
            }
            catch (Exception error)
            {
                return BadRequest(error);
                throw;
            }
        }
    }
}