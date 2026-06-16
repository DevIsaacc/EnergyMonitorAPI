using EnergyMonitorAPI.Services;
using EnergyMonitorAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyMonitorAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LeiturasController : ControllerBase
{
    private readonly LeituraService _service;
    public LeiturasController(LeituraService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Registrar([FromBody] LeituraRequest req)
    {
        var leitura = await _service.RegistrarAsync(req);
        return Ok(new { leitura.Id, leitura.ConsumoKWh, leitura.DataLeitura });
    }
}