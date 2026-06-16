using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EnergyMonitorAPI.Data;
using EnergyMonitorAPI.Models;
using EnergyMonitorAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EnergyMonitorAPI.Services;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;
    public AuthService(AppDbContext db, IConfiguration config) { _db = db; _config = config; }

    public async Task<TokenResponse?> LoginAsync(LoginRequest req)
    {
        var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.Email == req.Email);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(req.Senha, usuario.SenhaHash))
            return null;

        return new TokenResponse
        {
            Token = GerarToken(usuario),
            Nome = usuario.Nome,
            Role = usuario.Role
        };
    }

    public async Task<TokenResponse> RegisterAsync(RegisterRequest req)
    {
        var usuario = new Usuario
        {
            Nome = req.Nome,
            Email = req.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(req.Senha)
        };
        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync();
        return new TokenResponse { Token = GerarToken(usuario), Nome = usuario.Nome, Role = usuario.Role };
    }

    private string GerarToken(Usuario usuario)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Role)
        };
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}