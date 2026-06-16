using EnergyMonitorAPI.Services;
using EnergyMonitorAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyMonitorAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EquipamentosController : ControllerBase
{
    private readonly EquipamentoService _service;
    public EquipamentosController(EquipamentoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
    {
        var resultado = await _service.ListarAsync(pagina, tamanhoPagina);
        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] EquipamentoRequest req)
    {
        var resultado = await _service.CriarAsync(req);
        return CreatedAtAction(nameof(Listar), new { id = resultado.Id }, resultado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var ok = await _service.DeletarAsync(id);
        return ok ? NoContent() : NotFound();
    }
}