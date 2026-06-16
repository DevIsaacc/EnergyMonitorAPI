using EnergyMonitorAPI.Services;
using EnergyMonitorAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EnergyMonitorAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;
    public AuthController(AuthService service) => _service = service;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var token = await _service.LoginAsync(req);
        return token == null ? Unauthorized(new { mensagem = "Credenciais inválidas" }) : Ok(token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req)
    {
        var token = await _service.RegisterAsync(req);
        return Ok(token);
    }
}