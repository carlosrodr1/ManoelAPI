using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ManoelAPI.Models;
using ManoelAPI.Services;
using Microsoft.Extensions.Options;

namespace ManoelAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly JwtSettings _jwtSettings;

        public AuthController(UsuarioService usuarioService, IOptions<JwtSettings> jwtOptions)
        {
            _usuarioService = usuarioService;
            _jwtSettings = jwtOptions.Value;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioDTO dto)
        {
            var criado = await _usuarioService.RegistrarUsuario(dto);
            if (!criado)
                return BadRequest("Usuário já existe");

            return Ok("Usuário criado com sucesso");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO dto)
        {
            var usuarioValido = await _usuarioService.Autenticar(dto.Login, dto.Senha);
            if (usuarioValido == null)
                return Unauthorized("Usuário ou senha inválidos");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuarioValido.Login)
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours),
                signingCredentials: credenciais
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = tokenString });
        }
    }
}
