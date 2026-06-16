using EnergyMonitorAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyMonitorAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RelatoriosController : ControllerBase
{
    private readonly LeituraService _service;
    public RelatoriosController(LeituraService service) => _service = service;

    [HttpGet("eficiencia")]
    public async Task<IActionResult> Eficiencia([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
    {
        var relatorio = await _service.GerarRelatorioAsync(inicio, fim);
        return Ok(relatorio);
    }
}