using EnergyMonitorAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnergyMonitorAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AlertasController : ControllerBase
{
    private readonly AppDbContext _db;
    public AlertasController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
    {
        var query = _db.Alertas.Include(a => a.Equipamento).OrderByDescending(a => a.CriadoEm);
        var total = await query.CountAsync();
        var dados = await query.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina)
            .Select(a => new {
                a.Id,
                a.Mensagem,
                a.ConsumoRegistrado,
                a.Resolvido,
                a.CriadoEm,
                Equipamento = a.Equipamento.Nome
            }).ToListAsync();

        return Ok(new { pagina, totalPaginas = (int)Math.Ceiling(total / (double)tamanhoPagina), total, dados });
    }

    [HttpPatch("{id}/resolver")]
    public async Task<IActionResult> Resolver(int id)
    {
        var alerta = await _db.Alertas.FindAsync(id);
        if (alerta == null) return NotFound();
        alerta.Resolvido = true;
        await _db.SaveChangesAsync();
        return Ok(new { mensagem = "Alerta resolvido com sucesso" });
    }
}