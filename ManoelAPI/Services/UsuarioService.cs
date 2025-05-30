using ManoelAPI.Data;
using ManoelAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ManoelAPI.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> Autenticar(string login, string senha)
        {
            string hash = CalcularHash(senha);
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Login == login && u.SenhaHash == hash);
        }

        public static string CalcularHash(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public async Task<bool> RegistrarUsuario(UsuarioDTO dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Login == dto.Login))
                return false;

            var usuario = new Usuario
            {
                Login = dto.Login,
                SenhaHash = CalcularHash(dto.Senha)
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
