using EnergyMonitorAPI.Data;
using EnergyMonitorAPI.Models;
using EnergyMonitorAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EnergyMonitorAPI.Services;

public class EquipamentoService
{
    private readonly AppDbContext _db;
    public EquipamentoService(AppDbContext db) => _db = db;

    public async Task<PaginadoResponse<EquipamentoResponse>> ListarAsync(int pagina, int tamanhoPagina)
    {
        var query = _db.Equipamentos.Where(e => e.Ativo);
        var total = await query.CountAsync();

        var dados = await query
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .Select(e => new EquipamentoResponse
            {
                Id = e.Id,
                Nome = e.Nome,
                Setor = e.Setor,
                LimiteConsumoKWh = e.LimiteConsumoKWh,
                Ativo = e.Ativo
            }).ToListAsync();

        return new PaginadoResponse<EquipamentoResponse>
        {
            Pagina = pagina,
            TotalPaginas = (int)Math.Ceiling(total / (double)tamanhoPagina),
            TotalItens = total,
            Dados = dados
        };
    }

    public async Task<EquipamentoResponse> CriarAsync(EquipamentoRequest req)
    {
        var equipamento = new Equipamento
        {
            Nome = req.Nome,
            Setor = req.Setor,
            LimiteConsumoKWh = req.LimiteConsumoKWh
        };
        _db.Equipamentos.Add(equipamento);
        await _db.SaveChangesAsync();
        return new EquipamentoResponse
        {
            Id = equipamento.Id,
            Nome = equipamento.Nome,
            Setor = equipamento.Setor,
            LimiteConsumoKWh = equipamento.LimiteConsumoKWh,
            Ativo = equipamento.Ativo
        };
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var eq = await _db.Equipamentos.FindAsync(id);
        if (eq == null) return false;
        eq.Ativo = false;
        await _db.SaveChangesAsync();
        return true;
    }
}